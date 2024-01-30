using EasyNetQ;

internal class Program
{
    static async Task Main()
    {
        var connectionString = "host=localhost;username=guest;password=guest";

        using var bus = RabbitHutch.CreateBus(
            connectionString);

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