using RethinkDb.Driver;
using RethinkDb.Driver.Net;
using System.Linq;

namespace ProIdeas.Domain.Repositories.RethinkDb
{
    public class DefaultRethinkDbConnectionProvider : IRethinkDbConnectionProvider
    {

        public IConnection GetConnection(ConnectionOptions options)
        {
            return RethinkDB.R.Connection()
                .Db(options.DBName)
                .Port(options.Port)
                .Hostname(options.HostNames.First())
                .User(options.User, options.Password)
                .Connect();

        }
    }
}
