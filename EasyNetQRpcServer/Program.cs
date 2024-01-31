using Configuration;
using EasyNetQ;
using EasyNetQ.DI;

namespace EasyNetQRpcServer;

internal class Program
{
    static async Task Main()
    {
        var connectionString = "host=localhost;username=guest;password=guest";

        using var bus = RabbitHutch.CreateBus(
            connectionString,
            services =>
            {
                services.Register<IConventions>(
                    c => new MyConventions(c.Resolve<ITypeNameSerializer>()));
                
                services.Register<ITypeNameSerializer>(c => new MyTypeNameSerializer());

                services.Register<IMessageSerializationStrategy>(
                    c => new MyMessageSerializationStrategy(
                        c.Resolve<ITypeNameSerializer>(),
                        c.Resolve<ISerializer>()));
            });
        
        await bus.Rpc.RespondAsync<string, string>(request =>
        {
            Console.WriteLine($"Received: {request}");
            Console.WriteLine("Responding:");
            var response = $"Hello {request}";
            Console.WriteLine(response);
            return response;
        });
        
        Console.WriteLine("Listening for requests. Hit <return> to quit");
        Console.ReadLine();
    }
}