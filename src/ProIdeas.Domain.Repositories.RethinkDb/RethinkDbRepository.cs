using RethinkDb.Driver;
using RethinkDb.Driver.Net;
using RethinkDb.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RethinkDb.Driver.Ast;

namespace ProIdeas.Domain.Repositories.RethinkDb
{
    public class RethinkDbRepository : IRepository
    {
        private readonly IConnection _connection;
        private readonly ConnectionOptions _connectionOptions;


        public RethinkDbRepository(IRethinkDbConnectionProvider connectionProvider, ConnectionOptions connectionOptions)
        {
            _connectionOptions = connectionOptions;
            _connection = connectionProvider.GetConnection(_connectionOptions);          
        }

        private static string GetTableName<T>()
        {
            return typeof(T).Name;
        }

        private static Table GetTable<T>()
        {
            return RethinkDB.R.Table(GetTableName<T>());
        }



        public IEnumerable<T> Add<T>(params T[] items) where T : class, IEntity, new()
        {
            var result = GetTable<T>().Insert(items).RunResult<List<T>>(_connection);

            return result;
        }

        public IEnumerable<T> All<T>() where T : class, IEntity, new()
        {
            return AllQueryable<T>();
        }

        private IQueryable<T> AllQueryable<T>() where T : class, IEntity, new()
        {
            return RethinkDB.R.Db(_connectionOptions.DBName)
                                            .Table<T>(typeof(T).Name, _connection);
        }

        public IEnumerable<T> All<T>(int page, int pageSize) where T : class, IEntity, new()
        {
            return All<T>().Skip(page * pageSize).Take(pageSize).ToList();
        }

        public void Delete<T>(Expression<Func<T, bool>> expression) where T : class, IEntity, new()
        {
            var itemsToBeDeleted = AllQueryable<T>().Where(expression).Select(i => i.Id).ToList();

            RethinkDB.R.Table(GetTableName<T>()).GetAll(itemsToBeDeleted).Delete().RunResult(_connection);

        }

        public void Delete<T>(T item) where T : class, IEntity, new()
        {
            RethinkDB.R.Table(GetTableName<T>()).Get(item.Id).Delete().RunResult(_connection);
        }

        public void DeleteAll<T>() where T : class, IEntity, new()
        {
            RethinkDB.R.Table(GetTableName<T>()).Delete().RunResult(_connection);
        }

        public T Single<T>(Expression<Func<T, bool>> expression) where T : class, IEntity, new()
        {
            return AllQueryable<T>().SingleOrDefault(expression);
        }

        public T First<T>(Expression<Func<T, bool>> expression) where T : class, IEntity, new()
        {
            return AllQueryable<T>().FirstOrDefault(expression);
        }

       public IEnumerable<T> Query<T>(IQuery<T> query) where T : class, IEntity, new()
        {

            if (query == null)
            { throw new ArgumentNullException(nameof(query)); }

            var queryable = AllQueryable<T>();

            if (query.Filter != null)
            {
                queryable = queryable.Where(query.Filter);
            }

            queryable = query.OrderBy(queryable);

            if (query.Skip.HasValue)
            {
                queryable = queryable.Skip(query.Skip.Value);
            }

            if (query.Take.HasValue)
            {
                queryable = queryable.Skip(query.Take.Value);
            }


            return queryable.ToList();
        }

        public IEnumerable<T> Update<T>(params T[] items) where T : class, IEntity, new()
        {
            var result = GetTable<T>().Update(items).RunResult<List<T>>(_connection);

            return result;
        }
    }
}
