using DAApi.Interfaces;
using DAApi.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAApi.Services
{
    public class PublisherService : IPublisherService
    {
        public PublisherService()
        {

        }

        public int PublishToQueue(QueueItem item, string queue, string hostname)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = hostname };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = JsonSerializer.Serialize(item);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: queue,
                                         basicProperties: null,
                                         body: body);
                    Debug.WriteLine(" [x] Sent {0}", message);
                }
                return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return 1;
            }
        }
    }
}
