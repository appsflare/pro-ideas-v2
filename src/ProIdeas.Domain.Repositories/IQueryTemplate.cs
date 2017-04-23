using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Domain.Repositories
{
    public interface IQueryTemplate : IDisposable
    {
        string Name { get; }
    }


    public interface IQueryTemplate<TEntity, TQueryParam> : IQueryTemplate
        where TQueryParam : class
        where TEntity : class, new()
    {
        Task<IEnumerable<TEntity>> ExecuteAsync(dynamic connection, TQueryParam queryParam);
    }
}
