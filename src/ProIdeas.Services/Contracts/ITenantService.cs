using ProIdeas.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProIdeas.Services.Contracts
{
    public interface ITenantService
    {
        TenantSettingsDto GetTenant(string uniqueKey);

        TenantSettingsDto GetTenantByHostName(string hostName);

        IEnumerable<TenantSettingsDto> GetTenants();
    }
}
