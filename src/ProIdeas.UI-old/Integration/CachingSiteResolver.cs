using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SaasKit.Multitenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.UI
{
    public class CachingSiteResolver : MemoryCacheTenantResolver<TenantSettings>
    {
        private readonly IEnumerable<TenantSettings> tenants;

        public CachingSiteResolver(
            IMemoryCache cache,
            ILoggerFactory loggerFactory,
            IOptions<MultiTenancyOptions> options)
            : base(cache, loggerFactory)
        {
            tenants = options.Value.Tenants;
        }

        protected override string GetContextIdentifier(HttpContext context)
        {
            return context.Request.Host.Value.ToLower();
        }

        protected override IEnumerable<string> GetTenantIdentifiers(TenantContext<TenantSettings> context)
        {
            return context.Tenant.Hostnames;
        }

        protected override Task<TenantContext<TenantSettings>> ResolveAsync(HttpContext context)
        {
            TenantContext<TenantSettings> tenantContext = null;

            var tenant = tenants.FirstOrDefault(t =>
                t.Hostnames.Any(h => h.Equals(context.Request.Host.Value.ToLower())));

            if (tenant != null)
            {
                tenantContext = new TenantContext<TenantSettings>(tenant);
            }

            return Task.FromResult(tenantContext);
        }
    }

}
