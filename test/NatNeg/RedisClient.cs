namespace UniSpy.Redis.Test
{
    internal class RedisClient : UniSpyServer.LinqToRedis.RedisClient<NatNegUserInfo>
    {
        public RedisClient() : base("127.0.0.1:6789", 0)
        {
        }
    }
}