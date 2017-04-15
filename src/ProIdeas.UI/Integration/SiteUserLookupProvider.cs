using cloudscribe.Web.SimpleAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.UI
{
    public class SiteUserLookupProvider : IUserLookupProvider
    {
        public SiteUserLookupProvider(TenantSettings tenant)
        {
            this.tenant = tenant;
        }

        private TenantSettings tenant;

        public SimpleAuthUser GetUser(string userName)
        {
            foreach (SimpleAuthUser u in tenant.Users)
            {
                if (u.UserName == userName) { return u; }
            }

            return null;
        }
    }
}
