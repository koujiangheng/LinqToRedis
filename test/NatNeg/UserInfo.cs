using System;
using UniSpyServer.LinqToRedis;

namespace UniSpy.Redis.Test
{
    public record UserInfo : RedisKeyValueObject
    {
        public NatNegUserInfo() : base(TimeSpan.FromMinutes(3))
        {
            // we set the expire time to 3 minutes
        }
        [RedisKey]
        public Guid? ServerID { get; set; }
        [RedisKey]
        public int? Cookie { get; set; }
        public string UserName { get; set; }
        public string RemoteEndPoint { get; set; }
    }
}