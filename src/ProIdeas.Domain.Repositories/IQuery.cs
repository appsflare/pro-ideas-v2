using System;
using System.Linq;
using System.Linq.Expressions;

namespace ProIdeas.Domain.Repositories
{
    public interface IQuery<TEntity> where TEntity : IEntity
    {
        Expression<Func<TEntity, bool>> Filter { get; }

        IQueryable<TEntity> OrderBy(IQueryable<TEntity> queryable);

        int? Take { get; }

        int? Skip { get; }
    }
}
