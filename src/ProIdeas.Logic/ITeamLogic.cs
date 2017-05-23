using ProIdeas.Domain.Entities;
using ProIdeas.DTO;
using System.Threading.Tasks;

namespace ProIdeas.Logic
{
    public interface ITeamLogic
    {
        Task<TeamDto> GetTeamAsync(string ideaId);
        Task<TeamMemberDto> GetTeamMemberAsync(string userId, string ideaId);
    }
}
