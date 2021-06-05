import json
from types import SimpleNamespace
import pika
from Services import MongoService as ms
from Services import SwearService as ss

mongoFileService = None
swearAnalysisService = ss.SwearAnalysisService("Services/profanity.sav", "Services/vectorizer.sav")

def readConfig():
    f = open("appsettings.json")
    data = f.read()
    data = json.loads(data, object_hook=lambda d: SimpleNamespace(**d))
    f.close()
    return data

def makeAnalysis(item):
    result = swearAnalysisService.makeSwearAnalysis(item.Text)
    fileData = mongoFileService.get(item.Id)
    fileData["Result"] = result
    mongoFileService.update(item.Id, fileData)

    return

def receiveFromQueueAux(ch, method, props, body):
    print("Request received, starting Swear Analysis...")
    item = json.loads(body, object_hook=lambda d: SimpleNamespace(**d))

    makeAnalysis(item)

    response = "Successfully performed the Swear Analysis on " + item.Name
    print(response)

    ch.basic_publish(exchange='',
                     routing_key=props.reply_to,
                     properties=pika.BasicProperties(correlation_id = \
                                                         props.correlation_id),
                     body=str(response))
    ch.basic_ack(delivery_tag=method.delivery_tag)

    return

def receiveFromQueue(hostName, queueName):
    connection = pika.BlockingConnection(
    pika.ConnectionParameters(host=hostName))
    channel = connection.channel()
    channel.queue_declare(queue=queueName)

    channel.basic_qos(prefetch_count=1)
    channel.basic_consume(queue=queueName, on_message_callback=receiveFromQueueAux)

    print("Waiting for requests...")
    channel.start_consuming()
    return

def main():
    global mongoFileService

    config = readConfig()
    mongoFileService = ms.MongoFileService(config.MongoConnectionString, config.MongoDatabaseName, config.MongoCollectionName)
    
    receiveFromQueue(config.HostName, config.QueueName)
    return

if __name__ == "__main__":
    main()