# LinqToRedis
This small tool will give you the power to query redis key value data with Linq
This project is inspired by UniSpyServer

# Usage
The example will show below.
1. You need to create your class inherent from RedisKeyValueObject
2. Define some properties with \[RedisKey\] attribute
3. Create your own class which inherent from RedisClient<>, remember to specify the database
4. Use (1) GetValue method (2) index access (3) Linq to query from redis

# Example code
```    
    public record NatNegUserInfo : RedisKeyValueObject
    {
        [RedisKey]
        public Guid? ServerID { get; set; }
        [RedisKey]
        public int? Cookie { get; set; }
        public string UserName { get; set; }
        public string RemoteEndPoint { get; set; }
    }


    internal class RedisClient : UniSpyServer.LinqToRedis.RedisClient<NatNegUserInfo>
    {
        public RedisClient() : base("127.0.0.1:6789", 0)
        {
        }
    }
```
Then you can use the general way to query from redis.
```
    var key = new NatNegKey{ Cookie=1 };
    var client = new RedisClient();
    var results1 = client.GetValue(key);
    var results2 = client[key];
    var results3 = client.Where(x => x.Cookie == 1).ToList();
```
