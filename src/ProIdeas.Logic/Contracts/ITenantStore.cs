using ProIdeas.Domain.Entities;
using ProIdeas.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProIdeas.Logic.Contracts
{
    public interface ITenantStore
    {
        TenantSettings GetTenant(string uniqueKey);
    }
}
