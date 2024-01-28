using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQProduct.API.RabbitMQ
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5673,
                UserName = "admin",
                Password = "admin",
                VirtualHost = "/"
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("product", exclusive: false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "product", body: body);
        }
    }
}
