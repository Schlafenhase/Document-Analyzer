using DAApi.Configuration;
using DAApi.Hubs;
using DAApi.Hubs.Clients;
using DAApi.Interfaces;
using DAApi.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAApi.Services
{
    public class PublisherService
    {
        /// <summary>
        /// Atributes that handle services and configurations
        /// </summary>
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties props;
        private readonly IHubContext<ChatHub, IChatClient> _chatHub;
        private readonly RabbitMQConfig _rabbitMQConfig;

        /// <summary>
        /// Constructor of PublisherService
        /// </summary>
        /// <param name="chatHub">
        /// Connection associated with websockets
        /// </param>
        /// <param name="rabbitOptionsMonitor">
        /// RabbitMQ configuration
        /// </param>
        public PublisherService(IHubContext<ChatHub, IChatClient> chatHub, IOptionsMonitor<RabbitMQConfig> rabbitOptionsMonitor)
        {
            _rabbitMQConfig = rabbitOptionsMonitor.CurrentValue;
            _chatHub = chatHub;

            var factory = new ConnectionFactory() { HostName = _rabbitMQConfig.HostName };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);

            props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var response = Encoding.UTF8.GetString(body);
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    Debug.WriteLine(response);

                    ChatMessage chatMessage = new ChatMessage
                    {
                        User = "API",
                        Message = response
                    };

                    await _chatHub.Clients.All.ReceiveMessage(chatMessage);
                }
            };
        }

        /// <summary>
        /// Method that publishes an item to the queue
        /// </summary>
        /// <param name="item">
        /// Item to be published
        /// </param>
        /// <param name="queue">
        /// Queue that will save the item
        /// </param>
        /// <returns>
        /// Integer indicating if the operations was successful
        /// </returns>
        public int PublishToQueue(QueueItem item, string queue)
        {
            try
            {
                var messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(item));
                channel.BasicPublish(
                    exchange: "",
                    routingKey: queue,
                    basicProperties: props,
                    body: messageBytes);

                channel.BasicConsume(
                    consumer: consumer,
                    queue: replyQueueName,
                    autoAck: true);
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
