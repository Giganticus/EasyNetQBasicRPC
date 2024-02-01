using System;
using System.Threading;
using System.Threading.Tasks;
using Configuration;
using EasyNetQ;
using EasyNetQ.DI;

namespace EasyNetQRpcServer;

internal static class Program
{
    private static async Task Main()
    {
        const string connectionString = "host=localhost;username=guest;password=guest";

        using var bus = RabbitHutch.CreateBus(
            connectionString,
            services =>
            {
                services.Register<IConventions>(
                    c => new MyConventions(c.Resolve<ITypeNameSerializer>()));

                services.Register<IMessageSerializationStrategy>(
                    c => new MyMessageSerializationStrategy(
                        c.Resolve<ITypeNameSerializer>()));
            });
        
        await bus.Rpc.RespondAsync<string, string>((request, c) =>
        {
            Console.WriteLine($"Received: {request}");
            Console.WriteLine("Responding:");
            var response = $"Hello {request}";
            Console.WriteLine(response);
            return Task.FromResult(response);
        }, config =>
        {
            config.WithExpires(TimeSpan.FromSeconds(5));
            config.WithQueueName(string.Empty);

        }, CancellationToken.None);

        Console.WriteLine("Listening for requests. Hit <return> to quit");
        Console.ReadLine();
    }
}