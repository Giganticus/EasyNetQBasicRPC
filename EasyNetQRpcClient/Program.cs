using EasyNetQ;

class Program 
{
    static async Task Main() 
    {
        var connectionString = "host=localhost;username=guest;password=guest";

        using var bus = RabbitHutch.CreateBus(
            connectionString);
        {
            var input = String.Empty;
            Console.WriteLine("Enter a name to receive a greeting. 'Quit' to quit.");
            while ((input = Console.ReadLine()) != "Quit")
            {
                var response = await bus.Rpc.RequestAsync<string, string>(input);
                
                Console.WriteLine("Received:");
                Console.WriteLine(response);
            }
        }
    }
}