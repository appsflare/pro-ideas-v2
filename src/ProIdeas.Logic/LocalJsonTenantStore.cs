using ProIdeas.Domain.Entities;
using ProIdeas.Logic.Contracts;
using ProIdeas.Serializers.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProIdeas.Logic
{
    public class LocalJsonTenantStore : ITenantStore
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IEnumerable<TenantSettings> _tenants;
        public LocalJsonTenantStore(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
            _tenants = _jsonSerializer.Deserialize<IEnumerable<TenantSettings>>(File.ReadAllText("tenants.json"));
        }

        public TenantSettings GetTenantByHostName(string hostName)
        {
            return _tenants.FirstOrDefault(i => i.Hostnames.Any(h => h.Equals(hostName)));
        }

        public TenantSettings GetTenant(string uniqueKey)
        {
            return _tenants.FirstOrDefault(i => i.Id == uniqueKey);
        }

        public IEnumerable<TenantSettings> GetAllTenants()
        {
            return _tenants;
        }
    }
}
