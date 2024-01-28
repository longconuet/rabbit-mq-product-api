using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

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
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    var body = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Product mesage received: {message}");
};

channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);
Console.ReadKey();
