using EasyNetQ;

namespace Configuration;

public class MyConventions : Conventions
{
    public MyConventions(ITypeNameSerializer typeNameSerializer) : base(typeNameSerializer)
    {
        RpcRequestExchangeNamingConvention = x => "my.existing.exchange";
        RpcResponseExchangeNamingConvention = x => "";
        RpcRoutingKeyNamingConvention = x => x == typeof(string) ? string.Empty : base.RpcRoutingKeyNamingConvention(x);
    }
}