using ProIdeas.DTO;
using ProIdeas.Logic.Contracts;
using ProIdeas.Services.Contracts;
using System.Collections.Generic;

namespace ProIdeas.Services
{
    public class TenantService : ITenantService
    {
        private readonly ITenantLogic _tenantLogic;
        public TenantService(ITenantLogic tenantLogic)
        {
            _tenantLogic = tenantLogic;
        }
        public TenantSettingsDto GetTenant(string uniqueKey)
        {
            return _tenantLogic.GetTenant(uniqueKey);
        }

        public TenantSettingsDto GetTenantByHostName(string hostName)
        {
            return _tenantLogic.GetTenantByHostName(hostName);
        }

        public IEnumerable<TenantSettingsDto> GetTenants()
        {
            return _tenantLogic.GetAllTenants();
        }
    }
}
