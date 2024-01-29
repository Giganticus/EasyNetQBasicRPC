using Contracts;
using EasyNetQ;
using Newtonsoft.Json;

class Program 
{
    static void Main() 
    {
        var connectionString = "host=localhost;username=guest;password=guest";

        using var bus = RabbitHutch.CreateBus(connectionString);
        {
            var input = String.Empty;
            Console.WriteLine("Enter a name to receive a greeting. 'Quit' to quit.");
            while ((input = Console.ReadLine()) != "Quit")
            {
                var myRequest = new MyRequest(input);
                var response = bus.Rpc.Request<MyRequest, MyResponse>(myRequest);
                
                Console.WriteLine("Received:");
                Console.WriteLine(JsonConvert.SerializeObject(response));
            }
        }
    }
}