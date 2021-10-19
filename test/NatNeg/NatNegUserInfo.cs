using System;
using UniSpyServer.LinqToRedis;

namespace UniSpy.Redis.Test
{
    public record NatNegKey : RedisKeyValueObject
    {
        [RedisKey]
        public Guid? ServerID { get; set; }
        [RedisKey]
        public int? Cookie { get; set; }
    }
    public record NatNegUserInfo : NatNegKey
    {
        public string UserName { get; set; }
        public string RemoteEndPoint { get; set; }
    }
}