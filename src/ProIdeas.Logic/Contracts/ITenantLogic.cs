using ProIdeas.DTO;
using System.Collections.Generic;

namespace ProIdeas.Logic.Contracts
{
    public interface ITenantLogic
    {

        IEnumerable<TenantSettingsDto> GetAllTenants();

        TenantSettingsDto GetTenant(string uniqueKey);

        TenantSettingsDto GetTenantByHostName(string hostName);
    }
}
