using System;
using System.Collections.Generic;
using System.Linq;
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
            expression = QueryEvaluator.PartialEval(expression);
            var builder = new RedisQueryBuilder<TValue>(expression);
            builder.Build();

            var node = (MethodCallExpression)expression;
            if (node.Method.Name == "Count")
            {
                return _client.GetMatchedKeys(builder.KeyObject).Count;
            }
            var values = _client.GetValues(builder.KeyObject);

            switch (node.Method.Name)
            {
                case "Where":
                    return values;
                case "FirstOrDefault":
                    return values.FirstOrDefault();
                case "First":
                    if (values.Count == 0)
                    {
                        throw new InvalidOperationException("The result is empty, try to use FirstOrDefault instead.");
                    }
                    return values[0];
                // return values.First();
                default:
                    throw new NotSupportedException(string.Format("The method '{0}' is not supported", node.Method.Name));
            }
        }
    }
}