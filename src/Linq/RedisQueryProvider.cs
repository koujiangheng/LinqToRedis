using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UniSpyServer.LinqToRedis.Linq
{
    public class RedisQueryProvider<TValue> : QueryProviderBase where TValue : RedisKeyValueObject
    {
        private RedisClient<TValue> _client;
        public RedisQueryProvider(RedisClient<TValue> client) : base()
        {
            _client = client;
        }

        public override object Execute(Expression expression)
        {
            var matchedKeys = new List<string>();
            var builder = new RedisQueryBuilder<TValue>(expression);
            builder.Build();
            var values = _client.GetValues(builder.KeyObject);
            return values;
        }
    }
}