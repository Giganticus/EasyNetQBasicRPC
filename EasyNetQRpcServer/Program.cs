
using Contracts;
using EasyNetQ;
using Newtonsoft.Json;

internal class Program
{
    static void Main()
    {
        var connectionString = "host=localhost;username=guest;password=guest";

        using var bus = RabbitHutch.CreateBus(
            connectionString);

        bus.Rpc.Respond<MyRequest, MyResponse>(request =>
        {
            Console.WriteLine($"Received: {request.Name}");
            Console.WriteLine("Responding:");
            var response = new MyResponse($"Hello {request.Name}");
            Console.WriteLine(JsonConvert.SerializeObject(response));
            return response;
        });
        
        Console.WriteLine("Listening for requests. Hit <return> to quit");
        Console.ReadLine();
    }
}