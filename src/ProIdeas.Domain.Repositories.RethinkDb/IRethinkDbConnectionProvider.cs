using RethinkDb.Driver.Net;

namespace ProIdeas.Domain.Repositories.RethinkDb
{
    public interface IRethinkDbConnectionProvider
    {

        IConnection GetConnection(ConnectionOptions options);
    }
}
