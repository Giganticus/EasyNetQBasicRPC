namespace Contracts;

public class MyResponse(string greeting)
{
    public string Greeting { get; } = greeting;
}