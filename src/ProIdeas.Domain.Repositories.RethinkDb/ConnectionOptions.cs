using System.Collections.Generic;

namespace ProIdeas.Domain.Repositories.RethinkDb
{

    public class ConnectionOptions
    {

        public string DBName { get; set; }

        public IEnumerable<string> HostNames { get; set; }

        public int Port { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

    }
}
