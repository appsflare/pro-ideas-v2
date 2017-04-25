using RethinkDb.Driver.Net;

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
}
