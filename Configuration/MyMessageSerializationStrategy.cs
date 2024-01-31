using System.Text;
using EasyNetQ;
using EasyNetQ.Internals;

namespace Configuration;

public class MyMessageSerializationStrategy
    : IMessageSerializationStrategy
{
    private readonly ITypeNameSerializer _typeNameSerializer;

    public MyMessageSerializationStrategy( 
        ITypeNameSerializer typeNameSerializer)
    {
        _typeNameSerializer = typeNameSerializer;
    }
    
    public SerializedMessage SerializeMessage(IMessage message)
    {
        var stringType = typeof(string);
        
        var stream = new ArrayPooledMemoryStream();
        var body = (string)message.GetBody();
        var bytes = Encoding.UTF8.GetBytes(body);
        stream.Write(bytes);

        var messageProperties = message.Properties;
        messageProperties.Type = _typeNameSerializer.Serialize(stringType);
        
        var serializedMessage = new SerializedMessage(messageProperties, stream);
        return serializedMessage;
    }

    public IMessage DeserializeMessage(MessageProperties properties, in ReadOnlyMemory<byte> body)
    {
        var messageType = typeof(string);
        var messageBody = Encoding.UTF8.GetString(body.ToArray());
        return MessageFactory.CreateInstance(messageType, messageBody, properties);
    }
}