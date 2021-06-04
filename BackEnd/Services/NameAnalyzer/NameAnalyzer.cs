using DANameAnalyzer.Models;
using DANameAnalyzer.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace DANameAnalyzer
{
    class NameAnalyzer
    {
        private static DocumentAnalyzerDBContext _context;
        private static EmployeeService _employeeService;
        private static MongoFileService _mongoFileService;

        static void Main(string[] args)
        {
            Config config = ReadConfig();
            _context = new DocumentAnalyzerDBContext(config.SqlConnection);
            _employeeService = new EmployeeService(_context);
            _mongoFileService = new MongoFileService(config.MongoConnectionString, config.MongoDatabaseName, config.MongoCollectionName);

            ReceiveFromQueue(config.HostName, config.QueueName);    
        }

        private static Config ReadConfig()
        {
            using (StreamReader r = new("appsettings.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<Config>(json);
            }
        }

        private static void ReceiveFromQueue(string hostname, string queueName)
        {
            var factory = new ConnectionFactory() { HostName = hostname };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false,
                  exclusive: false, autoDelete: false, arguments: null);
                channel.BasicQos(0, 1, false);
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queue: queueName,
                  autoAck: false, consumer: consumer);
                Console.WriteLine("Waiting for requests...");

                consumer.Received += (model, ea) =>
                {
                    string response = null;

                    var body = ea.Body.ToArray();
                    var props = ea.BasicProperties;
                    var replyProps = channel.CreateBasicProperties();
                    replyProps.CorrelationId = props.CorrelationId;

                    try
                    {
                        var message = Encoding.UTF8.GetString(body);
                        QueueItem item = JsonConvert.DeserializeObject<QueueItem>(message);
                        Console.WriteLine("Request received, starting Name Analysis...");

                        MakeAnalysis(item);

                        response = "Successfully performed the Name Analysis on " + item.Name;
                        Console.WriteLine(response);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        response = "Error while processing the name analysis on a file";
                    }
                    finally
                    {
                        var responseBytes = Encoding.UTF8.GetBytes(response);
                        channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                          basicProperties: replyProps, body: responseBytes);
                        channel.BasicAck(deliveryTag: ea.DeliveryTag,
                          multiple: false);
                    }
                };

                Thread.Sleep(Timeout.Infinite);
                return;
            }
        }

        private static void MakeAnalysis(QueueItem item)
        {
            var doc = NameAnalysisService.MakeNameAnalysis(item.Text).Result;
            var result = NameAnalysisService.SearchEmployees(doc, _employeeService.GetEmployees());

            var fileData = _mongoFileService.Get(item.Id);
            fileData.NA = result;
            _mongoFileService.Update(item.Id, fileData);

            return;
        }
    }
}
