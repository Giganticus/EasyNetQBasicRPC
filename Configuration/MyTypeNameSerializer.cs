using EasyNetQ;

namespace Configuration;

public class MyTypeNameSerializer : ITypeNameSerializer
{
    public string Serialize(Type type)
    {
        return type.ToString();
    }

    public Type DeSerialize(string typeName)
    {
        return typeof(string);
    }
}