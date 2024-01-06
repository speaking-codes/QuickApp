using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.QueueService
{
    public class MessageQueueProducer : IMessageQueueProducer
    {
        private const string _hostName = "localhost";

        public void Send<T>(string queueName, T message)
        {
            var factory = new ConnectionFactory { HostName = _hostName };

            var connection = factory.CreateConnection();

            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queueName, true, false, false);
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: queueName, body: body);
            }
        }
    }
}
