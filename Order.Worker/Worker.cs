using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Shared;

namespace Order.Worker;

public class Worker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "order-queue",
            durable: false,
            exclusive: false,
            autoDelete: false);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);

            var order = JsonSerializer.Deserialize<OrderMessage>(json);

            Console.WriteLine($"Sipariþ alýndý: {order?.ProductName}");

            await Task.Delay(2000);

            Console.WriteLine("Sipariþ iþlendi");
        };

        await channel.BasicConsumeAsync(
            queue: "order-queue",
            autoAck: true,
            consumer: consumer);

        // Worker kapanmasýn diye bekletiyoruz
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}