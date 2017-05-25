using ProIdeas.DTO;
using System.Threading.Tasks;

namespace ProIdeas.Services
{
    public interface ITeamService
    {
        Task<TeamDto> GetTeamAsync(string ideaId);

        Task<TeamMemberDto> GetTeamMemberAsync(string userId, string ideaId); 

        Task RequestToJoinTeamAsync(string userId,string ideaId);

        Task ApproveJoinRequestAsync(string userId, string ideaId);

        Task RejectJoinRequestAsync(string userId, string ideaId);
    }          
}
