using ProIdeas.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProIdeas.Logic.Contracts
{
    public interface ITenantLogic
    {
        TenantSettingsDto GetTenant(string uniqueKey);
    }
}
