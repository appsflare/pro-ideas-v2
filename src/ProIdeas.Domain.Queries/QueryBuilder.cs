using ProIdeas.Domain.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ProIdeas.Domain.Queries
{
    public sealed class QueryBuilder<TEntity> where TEntity : IEntity
    {

        private Expression<Func<TEntity, bool>> _filter;
        private Func<IQueryable<TEntity>, IQueryable<TEntity>> _orderbyFunction;
        private int? _take, _skip;

        public QueryBuilder<TEntity> WithCondition(Expression<Func<TEntity, bool>> filter)
        {
            _filter = filter;
            return this;
        }

        public QueryBuilder<TEntity> OrderBy(Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBySelector)
        {
            _orderbyFunction = orderBySelector;
            return this;
        }


        public QueryBuilder<TEntity> Skip(int skip)
        {
            _skip = skip;
            return this;
        }

        public QueryBuilder<TEntity> Take(int take)
        {
            _take = take;
            return this;
        }



        public IQuery<TEntity> Build()
        {

            return new DynamicQuery(_orderbyFunction)
            {
                Filter = _filter,
                Skip = _skip,
                Take = _take
            };
        }


        private class DynamicQuery : IQuery<TEntity>
        {
            private readonly Func<IQueryable<TEntity>, IQueryable<TEntity>> _orderBySelector;
            public DynamicQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBySelector)
            {
                _orderBySelector = orderBySelector;
            }

            public Expression<Func<TEntity, bool>> Filter { get; set; }

            public int? Take { get; set; }

            public int? Skip { get; set; }

            public IQueryable<TEntity> OrderBy(IQueryable<TEntity> queryable)
            {
                return _orderBySelector == null ? queryable : _orderBySelector(queryable);
            }
        }

    }
}
