using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Shared;

namespace Order.API.Services;

public class RabbitMqService
{
    public async Task SendMessageAsync(OrderMessage message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "order-queue",
            durable: false,
            exclusive: false,
            autoDelete: false);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: "order-queue",
            body: body);
    }
}