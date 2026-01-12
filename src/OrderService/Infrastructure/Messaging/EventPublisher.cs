using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace OrderService.Infrastructure.Messaging;

public class EventPublisher
{
    public async Task PublishAsync<T>(T @event)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "order.created",
            durable: true,
            exclusive: false,
            autoDelete: false
        );

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: "order.created",
            body: body
        );
    }
}
