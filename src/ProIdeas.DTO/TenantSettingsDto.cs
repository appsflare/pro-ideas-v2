﻿namespace ProIdeas.DTO
{
    public class TenantSettingsDto : BaseDto<string>
    {
        public string UniqueKey { get; set; }
        public string SiteName { get; set; }
        public string[] Hostnames { get; set; }
        public string Theme { get; set; }

        public string ConnectionString { get; set; }

        public string ContentProjectId { get; set; }

        // this is just for compatibility with cloudscribe core
        // so themes can be interchangeable
        public string SiteFolderName { get; set; } = string.Empty;

        // overrides for default simpleauthsettings
        public bool EnablePasswordHasherUi { get; set; } = false;
        public string RecaptchaPublicKey { get; set; }
        public string RecaptchaPrivateKey { get; set; }
        public string AuthenticationScheme { get; set; } = "application";

    }
}
