using ProIdeas.Domain.Entities;

namespace ProIdeas.Logic.Contracts
{
    public interface ITenantStore
    {
        TenantSettings GetTenant(string uniqueKey);
    }
}
