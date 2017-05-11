using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Domain.Repositories
{


    public abstract class BaseQueryTemplate<TEntity, TQueryParam> : IQueryTemplate<TEntity, TQueryParam>
           where TQueryParam : class
        where TEntity : class, new()
    {
        public virtual string Name => typeof(TQueryParam).Name;


        public abstract Task<IEnumerable<TEntity>> ExecuteAsync(dynamic connection, TQueryParam queryParam);

    }
}
