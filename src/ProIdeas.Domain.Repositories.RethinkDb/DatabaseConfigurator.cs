using RethinkDb.Driver;
using RethinkDb.Driver.Net;
using System.Linq;
using System;
using System.Collections.Generic;

namespace ProIdeas.Domain.Repositories.RethinkDb
{

    public class DatabaseConfigurator
    {

        private readonly IRethinkDbConnectionProvider _connectionProvider;
        private readonly ConnectionOptions _connectionOptions;
        private Connection _connection;

        public DatabaseConfigurator(IRethinkDbConnectionProvider connectionProvider, ConnectionOptions connectionOptions)
        {
            _connectionProvider = connectionProvider;
            _connectionOptions = connectionOptions;

            _connection = _connectionProvider.GetConnection(_connectionOptions) as Connection;


        }

        private IConnection EnsureConnection()
        {
            if (!_connection.Open)
            {
                _connection.Reconnect();
            }

            return _connection;
        }


        public DatabaseConfigurator EnsureDB()
        {

            var dbExists = RethinkDB.R
                 .DbList()
                 .RunAtom<List<string>>(EnsureConnection()).ToList().Any(i => i == _connectionOptions.DBName);


            if (!dbExists)
            {
                if (RethinkDB.R.DbCreate(_connectionOptions.DBName).RunResult(EnsureConnection()).DatabasesCreated == 0)
                { throw new Exception("DB creation failed"); }
            }

            return this;
        }

        public DatabaseConfigurator EnsureTables(params string[] tables)
        {
            var allTables = RethinkDB.R
                 .TableList()
                 .RunAtom<List<string>>(EnsureConnection())
                 .ToList();

            var tablesToBeCreated = tables.Where(i => !allTables.Contains(i));


            foreach (var tableName in tablesToBeCreated)
            {
                if (RethinkDB.R.TableCreate(tableName).RunResult(EnsureConnection()).TablesCreated == 0)
                { throw new Exception("DB creation failed"); }
            }

            return this;
        }

        public DatabaseConfigurator EnsureTableIndex(string table, string index)
        {
            EnsureTables(table);

            var allIndices = RethinkDB.R
                 .Table(table)
                 .IndexList()
                 .RunAtom<List<string>>(EnsureConnection())
                 .ToList();

            if (!allIndices.Contains(index))
            { RethinkDB.R.Table(table).IndexCreate(index).Run(EnsureConnection()); }

            return this;
        }



    }
}
