using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace UniSpyServer.LinqToRedis.Linq
{
    public abstract class QueryProviderBase : IQueryProvider
    {
        public QueryProviderBase() { }
        IQueryable<T> IQueryProvider.CreateQuery<T>(Expression expression) => new QueryableObject<T>(this, expression);
        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            try
            {
                return (IQueryable)Activator.CreateInstance(typeof(QueryableObject<>).MakeGenericType(expression.Type), new object[] { this, expression });
            }
            catch (TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }
        T IQueryProvider.Execute<T>(Expression expression) => (T)Execute(expression);
        object IQueryProvider.Execute(Expression expression) => Execute(expression);
        public abstract object Execute(Expression expression);
    }
}