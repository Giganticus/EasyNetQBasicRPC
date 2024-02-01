using System;
using System.Threading.Tasks;
using Configuration;
using EasyNetQ;
using EasyNetQ.DI;

namespace EasyNetQRpcClient;

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
                
                services.Register<ITypeNameSerializer>(c => new MyTypeNameSerializer());

                services.Register<IMessageSerializationStrategy>(
                    c => new MyMessageSerializationStrategy(
                        c.Resolve<ITypeNameSerializer>()));
            });
        {
            string? input;
            Console.WriteLine("Enter a name to receive a greeting. 'Quit' to quit.");
            while ((input = Console.ReadLine()) != "Quit")
            {
                if(string.IsNullOrWhiteSpace(input))
                    continue;
                
                var response = await bus.Rpc.RequestAsync<string, string>(input);
                
                Console.WriteLine("Received:");
                Console.WriteLine(response);
            }
        }
    }
}