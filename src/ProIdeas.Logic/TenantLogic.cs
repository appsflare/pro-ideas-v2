using ProIdeas.Logic.Contracts;
using ProIdeas.DTO;
using ProIdeas.DataMappings.Data.Mappings.Contracts;

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

        public TenantSettingsDto GetTenant(string uniqueKey)
        {
            return _dataMapper.Map<TenantSettingsDto>(_tenantStore.GetTenant(uniqueKey));
        }
    }
}
