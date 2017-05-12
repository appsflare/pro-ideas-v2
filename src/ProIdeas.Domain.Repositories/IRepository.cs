using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProIdeas.Domain.Repositories
{
    public interface IRepository
    {
        void Delete<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity, new();

        void Delete<TEntity>(TEntity item) where TEntity : class, IEntity, new();

        Task DeleteAsync<TEntity>(TEntity item) where TEntity : class, IEntity, new();

        void DeleteAll<TEntity>() where TEntity : class, IEntity, new();

        TEntity Single<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity, new();

        TEntity First<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity, new();

        TEntity GetOne<TEntity>(string id) where TEntity : class, IEntity, new();

        Task<TEntity> GetOneAsync<TEntity>(string id) where TEntity : class, IEntity, new();

        IEnumerable<TEntity> All<TEntity>() where TEntity : class, IEntity, new();


        IEnumerable<TEntity> Query<TEntity>(IQuery<TEntity> query) where TEntity : class, IEntity, new();

        Task<IEnumerable<TEntity>> QueryAsync<TEntity, TQueryParam>(TQueryParam queryParam)
            where TEntity : class, new()
            where TQueryParam : class;

        Task<TEntity> QueryOneAsync<TEntity, TQueryParam>(TQueryParam queryParam)
            where TEntity : class, new()
            where TQueryParam : class;

        IEnumerable<TEntity> All<TEntity>(int page, int pageSize) where TEntity : class, IEntity, new();

        TEntity Add<TEntity>(TEntity item) where TEntity : class, IEntity, new();
        Task<TEntity> AddAsync<TEntity>(TEntity item) where TEntity : class, IEntity, new();
        TEntity Update<TEntity>(TEntity item) where TEntity : class, IEntity, new();
        Task<TEntity> UpdateAsync<TEntity>(TEntity item) where TEntity : class, IEntity, new();
    }
}
