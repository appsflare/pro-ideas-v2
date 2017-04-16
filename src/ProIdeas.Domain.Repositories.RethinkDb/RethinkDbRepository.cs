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

        private static string GetTableName<TEntity>()
        {
            return typeof(TEntity).Name;
        }

        private static Table GetTable<TEntity>()
        {
            return RethinkDB.R.Table(GetTableName<TEntity>());
        }



        public TEntity Add<TEntity>(TEntity item) where TEntity : class, IEntity, new()
        {
            return GetTable<TEntity>().Insert(item).RunAtom<TEntity>(_connection);
        }

        public IEnumerable<TEntity> All<TEntity>() where TEntity : class, IEntity, new()
        {
            return AllQueryable<TEntity>();
        }

        private IQueryable<TEntity> AllQueryable<TEntity>() where TEntity : class, IEntity, new()
        {
            return RethinkDB.R.Db(_connectionOptions.DBName)
                                            .Table<TEntity>(typeof(TEntity).Name, _connection);
        }

        public IEnumerable<TEntity> All<TEntity>(int page, int pageSize) where TEntity : class, IEntity, new()
        {
            return All<TEntity>().Skip(page * pageSize).Take(pageSize).ToList();
        }

        public void Delete<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity, new()
        {
            var itemsToBeDeleted = AllQueryable<TEntity>().Where(expression).Select(i => i.Id).ToList();

            RethinkDB.R.Table(GetTableName<TEntity>()).GetAll(itemsToBeDeleted).Delete().RunResult(_connection);

        }

        public void Delete<TEntity>(TEntity item) where TEntity : class, IEntity, new()
        {
            RethinkDB.R.Table(GetTableName<TEntity>()).Get(item.Id).Delete().RunResult(_connection);
        }

        public void DeleteAll<TEntity>() where TEntity : class, IEntity, new()
        {
            RethinkDB.R.Table(GetTableName<TEntity>()).Delete().RunResult(_connection);
        }

        public TEntity Single<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity, new()
        {
            return AllQueryable<TEntity>().SingleOrDefault(expression);
        }

        public TEntity First<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity, new()
        {
            return AllQueryable<TEntity>().FirstOrDefault(expression);
        }

        public IEnumerable<TEntity> Query<TEntity>(IQuery<TEntity> query) where TEntity : class, IEntity, new()
        {

            if (query == null)
            { throw new ArgumentNullException(nameof(query)); }

            var queryable = AllQueryable<TEntity>();

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

        public IEnumerable<TEntity> Update<TEntity>(params TEntity[] items) where TEntity : class, IEntity, new()
        {
            var result = GetTable<TEntity>().Update(items).RunResult<List<TEntity>>(_connection);

            return result;
        }

        TEntity IRepository.GetOne<TEntity>(string id)
        {
            return RethinkDB.R.Table(GetTableName<TEntity>()).Get(id).RunResult<TEntity>(_connection);
        }
    }
}
