using PlatformService.Dtos;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory
            {
                HostName = _configuration.GetValue<string>("RabbitMqHost"),
                Port = _configuration.GetValue<int>("RabbitMqPort"),    
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMqConnectionShutdown;

                Console.WriteLine("--> Connected to message bus");
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void RabbitMqConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> Rabbitmq Connection Shutdown");

        }

        public void PublishNewPlatform(PlatformPublishedDto platform)
        {
            var message = JsonSerializer.Serialize(platform);
            
            if(_connection.IsOpen)
            {
                Console.WriteLine("--> RabbitMq conecction is open, sending message");
                SendMessage(message);
            }
        }

        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
            Console.WriteLine($"Sent message: {message}");
        }

        public void Dispose()
        {
            Console.WriteLine("Message Bus disposed");
            if(_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}
