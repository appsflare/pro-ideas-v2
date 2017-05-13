using ProIdeas.Domain.Entities;
using System.Collections.Generic;

namespace ProIdeas.Logic.Contracts
{
    public interface ITenantStore
    {
        TenantSettings GetTenant(string uniqueKey);

        TenantSettings GetTenantByHostName(string hostName);

        IEnumerable<TenantSettings> GetAllTenants();
    }
}
