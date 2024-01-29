namespace Contracts;

public class MyRequest(string name)
{
    public string Name { get; } = name;
}