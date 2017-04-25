using System.Linq;
using System.Collections.Generic;

namespace ProIdeas.Domain.Repositories
{
    public class DefaultQueryTemplateFinder : IQueryTemplateFinder
    {
        private readonly IEnumerable<IQueryTemplate> _queryTemplates;
        public DefaultQueryTemplateFinder(IEnumerable<IQueryTemplate> queryTemplates)
        {
            _queryTemplates = queryTemplates;
        }

        public IQueryTemplate<TEntity, TQueryParam> Find<TEntity, TQueryParam>()
            where TEntity : class, new()
            where TQueryParam : class
        {
            return _queryTemplates.FirstOrDefault(i => i.Name == typeof(TQueryParam).Name) as IQueryTemplate<TEntity, TQueryParam>;
        }       
    }
}
