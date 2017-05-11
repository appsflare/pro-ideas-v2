using ProIdeas.DTO;

namespace ProIdeas.Logic.Contracts
{
    public interface ITenantLogic
    {
        TenantSettingsDto GetTenant(string uniqueKey);
    }
}
