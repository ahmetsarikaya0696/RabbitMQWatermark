using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQWatermark.Services
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMQClientService _rabbitmqClientService;

        public RabbitMQPublisher(RabbitMQClientService rabbitmqClientService)
        {
            _rabbitmqClientService = rabbitmqClientService;
        }

        public void Publish(ProductImageCreatedEvent productImageCreatedEvent)
        {
            var channel = _rabbitmqClientService.Connect();

            var messageString = JsonSerializer.Serialize(productImageCreatedEvent);
            var messageBody = Encoding.UTF8.GetBytes(messageString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(RabbitMQClientService.ExchangeName, RabbitMQClientService.RoutingKey, properties, messageBody);
        }
    }
}
