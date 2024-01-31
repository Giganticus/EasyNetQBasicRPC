using EasyNetQ;

namespace Configuration;

public class MyConventions : Conventions
{
    public MyConventions(ITypeNameSerializer typeNameSerializer) : base(typeNameSerializer)
    {
        ExchangeNamingConvention = x => "my.existing.exchange";
        RpcRequestExchangeNamingConvention = x => "my.existing.exchange";
        RpcRoutingKeyNamingConvention = x => x == typeof(string) ? string.Empty : base.RpcRoutingKeyNamingConvention(x);
    }
}