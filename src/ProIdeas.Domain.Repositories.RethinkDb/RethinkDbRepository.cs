﻿using RethinkDb.Driver;
using RethinkDb.Driver.Net;
using RethinkDb.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RethinkDb.Driver.Ast;
using System.Threading.Tasks;

namespace ProIdeas.Domain.Repositories.RethinkDb
{
    public class RethinkDbRepository : IRepository
    {
        private readonly IConnection _connection;
        private readonly ConnectionOptions _connectionOptions;
        private readonly IQueryTemplateFinder _queryTemplateProvider;


        public RethinkDbRepository(IRethinkDbConnectionProvider connectionProvider,
            ConnectionOptions connectionOptions,
            IQueryTemplateFinder queryTemplateProvider)
        {
            _connectionOptions = connectionOptions;
            _connection = connectionProvider.GetConnection(_connectionOptions);
            _queryTemplateProvider = queryTemplateProvider;
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
            var result = GetTable<TEntity>().Insert(item).RunResult(_connection);
            return result.GeneratedKeys.Any() ? GetOne<TEntity>(result.GeneratedKeys.First().ToString()) : default(TEntity);
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

        public Task DeleteAsync<TEntity>(TEntity item) where TEntity : class, IEntity, new()
        {
            return RethinkDB.R.Table(GetTableName<TEntity>()).Get(item.Id).Delete().RunResultAsync(_connection);
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

        public TEntity Update<TEntity>(TEntity item) where TEntity : class, IEntity, new()
        {

            var result = RethinkDB.R.Table(GetTableName<TEntity>()).Get(item.Id).Update(item).RunResult(_connection);
            return GetOne<TEntity>(item.Id);
        }

        public TEntity GetOne<TEntity>(string id) where TEntity : class, IEntity, new()
        {
            return RethinkDB.R.Table(GetTableName<TEntity>()).Get(id).RunResult<TEntity>(_connection);
        }

        public Task<TEntity> GetOneAsync<TEntity>(string id) where TEntity : class, IEntity, new()
        {
            return RethinkDB.R.Table(GetTableName<TEntity>()).Get(id).RunResultAsync<TEntity>(_connection);
        }

        public Task<IEnumerable<TEntity>> QueryAsync<TEntity, TQueryParam>(TQueryParam queryParam)
            where TEntity : class, new()
            where TQueryParam : class
        {
            var queryTemplate = _queryTemplateProvider.Find<TEntity, TQueryParam>();

            return queryTemplate?.ExecuteAsync(_connection, queryParam);
        }


        public async Task<TEntity> QueryOneAsync<TEntity, TQueryParam>(TQueryParam queryParam)
                where TEntity : class, new()
                where TQueryParam : class
        {
            var queryTemplate = _queryTemplateProvider.Find<TEntity, TQueryParam>();

            return (await queryTemplate?.ExecuteAsync(_connection, queryParam)).FirstOrDefault();
        }

        async Task<TEntity> IRepository.AddAsync<TEntity>(TEntity item)
        {
            var result = await GetTable<TEntity>().Insert(item).RunResultAsync(_connection);
            return result.GeneratedKeys.Any() ? await GetOneAsync<TEntity>(result.GeneratedKeys.First().ToString()) : default(TEntity);
        }

        async Task<TEntity> IRepository.UpdateAsync<TEntity>(TEntity item)
        {
            var result = await RethinkDB.R.Table(GetTableName<TEntity>()).Get(item.Id).Update(item).RunResultAsync(_connection);
            return await GetOneAsync<TEntity>(item.Id);
        }
    }
}
