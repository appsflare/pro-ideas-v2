using RethinkDb.Driver.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProIdeas.Domain.Repositories.RethinkDb
{
   public interface IRethinkDbConnectionProvider
    {

        IConnection GetConnection(ConnectionOptions options);
    }
}
