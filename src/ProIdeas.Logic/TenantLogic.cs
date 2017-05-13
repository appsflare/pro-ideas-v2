using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.DTO;
using ProIdeas.Logic.Contracts;
using System.Collections.Generic;

namespace ProIdeas.Logic
{
    public class TenantLogic : ITenantLogic
    {
        private readonly ITenantStore _tenantStore;
        private readonly IDataMapper _dataMapper;
        public TenantLogic(ITenantStore tenantStore, IDataMapper dataMapper)
        {
            _tenantStore = tenantStore;
            _dataMapper = dataMapper;
        }

        public IEnumerable<TenantSettingsDto> GetAllTenants()
        {
            return _dataMapper.Map<IEnumerable<TenantSettingsDto>>(_tenantStore.GetAllTenants());
        }

        public TenantSettingsDto GetTenant(string uniqueKey)
        {
            return _dataMapper.Map<TenantSettingsDto>(_tenantStore.GetTenant(uniqueKey));
        }

        public TenantSettingsDto GetTenantByHostName(string hostName)
        {
            return _dataMapper.Map<TenantSettingsDto>(_tenantStore.GetTenantByHostName(hostName));
        }
    }
}
