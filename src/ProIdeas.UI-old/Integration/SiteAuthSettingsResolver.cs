using cloudscribe.Web.SimpleAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.UI
{
    public class SiteAuthSettingsResolver : IAuthSettingsResolver
    {
        public SiteAuthSettingsResolver(TenantSettings tenant)
        {
            this.tenant = tenant;

            authSettings = new SimpleAuthSettings()
            {
                AuthenticationScheme = tenant.AuthenticationScheme,
                RecaptchaPrivateKey = tenant.RecaptchaPrivateKey,
                RecaptchaPublicKey = tenant.RecaptchaPublicKey,
                EnablePasswordHasherUi = tenant.EnablePasswordHasherUi
            };
        }

        private TenantSettings tenant;
        private SimpleAuthSettings authSettings;

        public SimpleAuthSettings GetCurrentAuthSettings()
        {
            return authSettings;
        }
    }
}
