using System;
using System.Collections.Generic;
using System.Text;

namespace ProIdeas.Domain.Repositories.RethinkDb
{

    public class ConnectionOptions
    {

        public string DBName { get; set; }

        public IEnumerable<string> HostNames { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

    }
}
