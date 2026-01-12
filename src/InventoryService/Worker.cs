using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using InventoryService.Contracts;

namespace InventoryService;

public class Worker : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: "order.created",
            durable: true,
            exclusive: false,
            autoDelete: false
        );

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, ea) =>
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            var orderCreated =
                JsonSerializer.Deserialize<OrderCreatedEvent>(json);

            Console.WriteLine(
                $"[InventoryService] Received OrderCreated for OrderId: {orderCreated?.OrderId}"
            );

            channel.BasicAck(ea.DeliveryTag, false);
        };

        channel.BasicConsume(
            queue: "order.created",
            autoAck: false,
            consumer: consumer
        );

        return Task.CompletedTask;
    }
}
