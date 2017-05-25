using ProIdeas.Domain.Core.Bus;
using ProIdeas.Infra.Commands.Team;
using System.Threading.Tasks;
using ProIdeas.DTO;
using ProIdeas.Logic;

namespace ProIdeas.Services
{
    public class TeamService : ITeamService
    {
        private readonly IBus _bus;
        private readonly ITeamLogic _teamLogic;

        public TeamService(IBus bus, ITeamLogic teamLogic)
        {
            _bus = bus;
            _teamLogic = teamLogic;
        }

        public async Task<TeamDto> GetTeamAsync(string ideaId)
        {
            return await _teamLogic.GetTeamAsync(ideaId);
        }

        public async Task<TeamMemberDto> GetTeamMemberAsync(string userId, string ideaId)
        {
            return await _teamLogic.GetTeamMemberAsync(userId, ideaId);
        }

        public Task ApproveJoinRequestAsync(string userId, string ideaId)
        {
            return _bus.SendCommand(new ApproveJoinTeamRequestCommand(userId, ideaId));
        }

        public Task RejectJoinRequestAsync(string userId, string ideaId)
        {
            return _bus.SendCommand(new RejectJoinTeamRequestCommand(userId, ideaId));
        }

        public Task RequestToJoinTeamAsync(string userId, string ideaId)
        {
            return _bus.SendCommand(new JoinTeamRequestCommand(userId, ideaId));
        }

        
    }
}
