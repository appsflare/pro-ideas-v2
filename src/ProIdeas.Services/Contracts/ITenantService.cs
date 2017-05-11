using ProIdeas.DTO;
using System.Collections.Generic;

namespace ProIdeas.Services.Contracts
{
    public interface ITenantService
    {
        TenantSettingsDto GetTenant(string uniqueKey);

        TenantSettingsDto GetTenantByHostName(string hostName);

        IEnumerable<TenantSettingsDto> GetTenants();
    }
}
