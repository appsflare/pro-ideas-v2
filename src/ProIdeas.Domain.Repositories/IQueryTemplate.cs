using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Domain.Repositories
{
    public interface IQueryTemplate
    {
        string Name { get; }
    }


    public interface IQueryTemplate<TEntity, TQueryParam> : IQueryTemplate
        where TQueryParam : class
        where TEntity : class, new()
    {
        Task<IEnumerable<TEntity>> ExecuteAsync(dynamic connection, TQueryParam queryParam);
    }

    //public interface IQueryOneTemplate<TEntity, TQueryParam> : IQueryTemplate
    //    where TQueryParam : class
    //    where TEntity : class, new()
    //{
    //    Task<TEntity> ExecuteAsync(dynamic connection, TQueryParam queryParam);
    //}
}
