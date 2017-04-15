using System;
using System.Collections.Generic;
using System.Text;

namespace ProIdeas.Domain.Entities
{

    public class UserEmail
    {
        public string Type { get; set; }

        public string Value { get; set; }
    }

    public class User:BaseEntity<string>
    {
        public string UserName { get; set; }

        public string PrimaryEmail { get; set; }

        public IList<UserEmail> Emails { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
