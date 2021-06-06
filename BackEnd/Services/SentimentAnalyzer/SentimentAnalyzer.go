package main

import (
	"bytes"
	"context"
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"net/http"

	"github.com/streadway/amqp"
	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

type Config struct {
	HostName              string
	QueueName             string
	MongoCollectionName   string
	MongoConnectionString string
	MongoDatabaseName     string
	APIUrl                string
}

type QueueItem struct {
	Id   int
	Name string
	Text string
}

func failOnError(err error, msg string) {
	if err != nil {
		log.Fatalf("%s: %s", msg, err)
	}
}

func readConfig() Config {
	jsonData, err := ioutil.ReadFile("appsettings.json")
	if err != nil {
		fmt.Print(err)
	}

	var data Config

	err = json.Unmarshal(jsonData, &data)
	if err != nil {
		fmt.Println("error:", err)
	}

	return data
}

func connectToMongo(connectionString string, databaseName string, collectionName string) (*mongo.Collection, context.Context) {
	client, err := mongo.NewClient(options.Client().ApplyURI(connectionString))
	if err != nil {
		log.Fatal(err)
	}

	ctx := context.Background()
	err = client.Connect(ctx)
	if err != nil {
		log.Fatal(err)
	}
	collection := client.Database(databaseName).Collection(collectionName)

	return collection, ctx
}

func makeAnalysisAux(text string, url string) (float64, string) {
	values := map[string]string{"text": text}
	json_data, err := json.Marshal(values)

	if err != nil {
		log.Fatal(err)
	}

	resp, err := http.Post(url, "application/json",
		bytes.NewBuffer(json_data))

	if err != nil {
		log.Fatal(err)
	}

	var res map[string]interface{}

	json.NewDecoder(resp.Body).Decode(&res)

	percentage := res["result"].(map[string]interface{})["polarity"].(float64)
	percentage *= 100

	message := res["result"].(map[string]interface{})["type"].(string)

	return percentage, message
}

func makeAnalysis(item QueueItem, url string, mongoFiles *mongo.Collection, ctx context.Context) {
	resultPercentage, resultMessage := makeAnalysisAux(item.Text, url)

	mongoFiles.FindOneAndReplace(ctx, bson.M{"FileId": item.Id}, bson.M{"FileId": item.Id, "ResultPercentage": int(resultPercentage), "ResultMessage": resultMessage})

	return
}

func receiveFromQueue(hostName string, queueName string, apiURL string, mongoFiles *mongo.Collection, ctx context.Context) {
	conn, err := amqp.Dial("amqp://guest:guest@" + hostName + ":5672/")
	failOnError(err, "Failed to connect to RabbitMQ")
	defer conn.Close()

	ch, err := conn.Channel()
	failOnError(err, "Failed to open a channel")
	defer ch.Close()

	q, err := ch.QueueDeclare(
		queueName, // name
		false,     // durable
		false,     // delete when unused
		false,     // exclusive
		false,     // no-wait
		nil,       // arguments
	)
	failOnError(err, "Failed to declare a queue")

	err = ch.Qos(
		1,     // prefetch count
		0,     // prefetch size
		false, // global
	)
	failOnError(err, "Failed to set QoS")

	msgs, err := ch.Consume(
		q.Name, // queue
		"",     // consumer
		false,  // auto-ack
		false,  // exclusive
		false,  // no-local
		false,  // no-wait
		nil,    // args
	)
	failOnError(err, "Failed to register a consumer")

	forever := make(chan bool)

	go func() {
		for d := range msgs {
			var item QueueItem

			err = json.Unmarshal(d.Body, &item)
			if err != nil {
				fmt.Println("error:", err)
			}

			fmt.Println("Request received, starting Sentiment Analysis...")

			makeAnalysis(item, apiURL, mongoFiles, ctx)

			response := "Successfully performed the Sentiment Analysis on " + item.Name
			fmt.Println(response)

			err = ch.Publish(
				"",        // exchange
				d.ReplyTo, // routing key
				false,     // mandatory
				false,     // immediate
				amqp.Publishing{
					ContentType:   "text/plain",
					CorrelationId: d.CorrelationId,
					Body:          []byte(response),
				})
			failOnError(err, "Failed to publish a message")

			d.Ack(false)
		}
	}()

	fmt.Println("Waiting for requests...")
	<-forever
}

func main() {
	config := readConfig()
	mongoFiles, ctx := connectToMongo(config.MongoConnectionString, config.MongoDatabaseName, config.MongoCollectionName)

	receiveFromQueue(config.HostName, config.QueueName, config.APIUrl, mongoFiles, ctx)

	return
}
