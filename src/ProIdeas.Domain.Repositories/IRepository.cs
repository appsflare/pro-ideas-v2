using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProIdeas.Domain.Repositories
{
    public interface IRepository
    {
        void Delete<T>(Expression<Func<T, bool>> expression) where T : class, IEntity, new();

        void Delete<T>(T item) where T : class, IEntity, new();

        void DeleteAll<T>() where T : class, IEntity, new();

        T Single<T>(Expression<Func<T, bool>> expression) where T : class, IEntity, new();

        T First<T>(Expression<Func<T, bool>> expression) where T : class, IEntity, new();

        IEnumerable<T> All<T>() where T : class, IEntity, new();


        IEnumerable<T> Query<T>(IQuery<T> query) where T : class, IEntity, new();

        IEnumerable<T> All<T>(int page, int pageSize) where T : class, IEntity, new();

        IEnumerable<T> Add<T>(params T[] items) where T : class, IEntity, new();

        IEnumerable<T> Update<T>(params T[] items) where T : class, IEntity, new();
    }
}
