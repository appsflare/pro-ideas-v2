using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ProIdeas.DTO;
using ProIdeas.Services.Contracts;
using SaasKit.Multitenancy;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.UI
{
    public class CachingTenantResolver : MemoryCacheTenantResolver<TenantSettingsDto>
    {
        private readonly IEnumerable<TenantSettingsDto> _tenants;

        public CachingTenantResolver(
            IMemoryCache cache,
            ILoggerFactory loggerFactory,
            ITenantService tenantService)
            : base(cache, loggerFactory)
        {
            _tenants = tenantService.GetTenants();
        }

        protected override string GetContextIdentifier(HttpContext context)
        {
            return context.Request.Host.Value.ToLower();
        }

        protected override IEnumerable<string> GetTenantIdentifiers(TenantContext<TenantSettingsDto> context)
        {
            return context.Tenant.Hostnames;
        }

        protected override Task<TenantContext<TenantSettingsDto>> ResolveAsync(HttpContext context)
        {
            TenantContext<TenantSettingsDto> tenantContext = null;

            var tenant = _tenants.FirstOrDefault(t => t.Hostnames.Any(h => h.Equals(context.Request.Host.Value.ToLower())));

            if (tenant != null)
            {
                tenantContext = new TenantContext<TenantSettingsDto>(tenant);
            }

            return Task.FromResult(tenantContext);
        }
    }

}
