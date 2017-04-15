using ProIdeas.Services.Contracts;
using System;
using ProIdeas.DTO;
using System.Collections.Generic;
using System.Linq;

namespace ProIdeas.Services
{
    public class TenantService : ITenantService
    {
        public TenantSettingsDto GetTenant(string uniqueKey)
        {
            throw new NotImplementedException();
        }

        public TenantSettingsDto GetTenantByHostName(string hostName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TenantSettingsDto> GetTenants()
        {
            return Enumerable.Empty<TenantSettingsDto>();
        }
    }
}
