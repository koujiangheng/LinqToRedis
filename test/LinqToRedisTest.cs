using System;
using System.Linq;
using UniSpyServer.LinqToRedis;
using Xunit;

namespace UniSpy.Redis.Test
{
    public class LinqToRedisTest
    {
        [Fact]
        public void ReadTest1()
        {
            var redis = new RedisClient();
            var data2 = redis.Values.Where(k => k.Cookie == 0).ToList();
        }
        [Fact]
        public void ReadTest2()
        {
            var redis = new RedisClient();
            var data2 = redis.Values.Where(k => k.RemoteEndPoint == "127.0.0.1:7777").ToList();
            Console.WriteLine(data2.Count);
        }
        [Fact]
        public void WriteTest1()
        {
            var redis = new RedisClient();
            var value = new NatNegUserInfo
            {
                Cookie = 0,
                ServerID = new Guid(),
                UserName = "hello2",
                RemoteEndPoint = "127.0.0.1:7890"
            };

            redis.SetValue(value);
        }
        [Fact]
        public void WriteTest2()
        {
            var redis = new RedisClient();

            for (int i = 0; i < 100; i++)
            {
                var value = new NatNegUserInfo
                {
                    Cookie = 0,
                    ServerID = new Guid(),
                    UserName = "hello2",
                    RemoteEndPoint = "127.0.0.1:7890"
                };
                redis.SetValue(value);
            }
        }
        [Fact]
        public void IndexWriteTest()
        {
            var redis = new RedisClient();

            for (int i = 0; i < 100; i++)
            {
                var value = new NatNegUserInfo
                {
                    Cookie = 0,
                    ServerID = new Guid(),
                    UserName = "hello2",
                    RemoteEndPoint = "127.0.0.1:7890"
                };
                // you can directly using value object to set value
                redis[value] = value;
                // or for understanding you can convert value object to IRedisKey
                var key = (IRedisKey)value;
                redis[key] = value;
            }
        }
        [Fact]
        public void IndexReadTest()
        {
            var redis = new RedisClient();

            for (int i = 0; i < 100; i++)
            {
                var key = new NatNegUserInfo
                {
                    Cookie = i,
                    ServerID = new Guid()
                };
                var value = redis[key];
            }
        }
    }
}
