using ProIdeas.Domain.Repositories;
using RethinkDb.Driver.Net;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates
{
    public class QueryTemplateContext<TQueryParam> where TQueryParam : class
    {

        internal QueryTemplateContext(IConnection connection, TQueryParam queryParam)
        {
            Connection = connection;
            Parameter = queryParam;
        }

        public IConnection Connection { get; private set; }

        public TQueryParam Parameter { get; private set; }

    }

    public abstract class BaseRethinkQueryTemplate<TEntity, TQueryParam> : BaseQueryTemplate<TEntity, TQueryParam>
        where TQueryParam : class
        where TEntity : class, new()
    {

        public override Task<IEnumerable<TEntity>> ExecuteAsync(dynamic connection, TQueryParam queryParam)
        {
            return ExecuteAsync(new QueryTemplateContext<TQueryParam>(connection, queryParam));
        }

        protected abstract Task<IEnumerable<TEntity>> ExecuteAsync(QueryTemplateContext<TQueryParam> context);

    }
}
