using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using ProIdeas.Domain.Repositories;
using ProIdeas.DTO;
using ProIdeas.Exceptions;
using ProIdeas.Infra.Commands.Team;
using ProIdeas.Infra.Events;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ProIdeas.Logic
{
    public class TeamLogic : ITeamLogic, IHandler<JoinTeamRequestCommand>,
        IHandler<ApproveJoinTeamRequestCommand>,
        IHandler<RejectJoinTeamRequestCommand>,
        IHandler<IdeaCreatedEvent>
    {
        private readonly IRepository _repository;
        private IDataMapper _dataMapper;
        private readonly IBus _bus;

        public TeamLogic(IRepository repository, IDataMapper dataMapper, IBus bus)
        {
            _repository = repository;
            _dataMapper = dataMapper;
            _bus = bus;
        }

        async public Task<TeamDto> GetTeamAsync(string ideaId)
        {
            return await GetTeam(ideaId);
        }

        private async Task<TeamDto> GetTeam(string ideaId)
        {
            Team team = await GetTeamByIdeaId(ideaId);

            if (team == null)
            { throw new LogicalException(ErrorCategory.NotFound, "Team not found"); }

            return _dataMapper.Map<TeamDto>(team);
        }

        private async Task<Team> GetTeamByIdeaId(string ideaId)
        {
            return await _repository.QueryOneAsync<Team, GetTeamByIdeaIdQuery>(new GetTeamByIdeaIdQuery
            {
                IdeaId = ideaId
            });
        }

        async public Task<TeamMemberDto> GetTeamMemberAsync(string userId, string ideaId)
        {
            var teamMember = await _repository.QueryOneAsync<TeamMember, GetTeamMemberByUserIdQuery>(new GetTeamMemberByUserIdQuery
            {
                IdeaId = ideaId,
                UserId = userId,
            });

            if (teamMember == null)
            {
                return null;
            }

            return _dataMapper.Map<TeamMemberDto>(teamMember);
        }

        async public Task Handle(JoinTeamRequestCommand message)
        {
            var team = await GetTeamByIdeaId(message.IdeaId);
            if (team == null)
            {
                throw new LogicalException(ErrorCategory.NotFound, "Team not found");
            }
            var teamMember = team.Members.FirstOrDefault(x => x.MemberUserId.Equals(message.UserId));
            if (teamMember != null)
            {
                throw new LogicalException(ErrorCategory.Logic, "Team member already exist");
            }
            team.Members.Add(new TeamMember
            {
                MemberUserId = message.UserId,
                Status = Domain.Entities.MemberStatus.Request
            });
            await _repository.UpdateAsync(team);
            await _bus.RaiseEvent(new TeamChangedEvent(_dataMapper.Map<TeamDto>(team)));
        }

        async public Task Handle(ApproveJoinTeamRequestCommand message)
        {
            var team = await GetTeamByIdeaId(message.IdeaId);
            if (team == null)
            {
                throw new LogicalException(ErrorCategory.NotFound, "Team not found");
            }
            team.Members.FirstOrDefault(x => x.MemberUserId.Equals(message.UserId)).Status = Domain.Entities.MemberStatus.Approved;
            _repository.Update(team);
            await _bus.RaiseEvent(new TeamChangedEvent(_dataMapper.Map<TeamDto>(team)));
        }

        async public Task Handle(RejectJoinTeamRequestCommand message)
        {
            var team = await GetTeamByIdeaId(message.IdeaId);
            if (team == null)
            {
                throw new LogicalException(ErrorCategory.NotFound, "Team not found");
            }
            var rejectedTeamMember = team.Members.FirstOrDefault(x => x.MemberUserId.Equals(message.UserId));
            team.Members.Remove(rejectedTeamMember);
            await _repository.UpdateAsync(team);
            await _bus.RaiseEvent(new TeamChangedEvent(_dataMapper.Map<TeamDto>(team)));
        }

        async public Task Handle(IdeaCreatedEvent message)
        {
            var team = new Team
            {
                IdeaId = message.Idea.Id
            };
            var addedTeam  = await _repository.AddAsync<Team>(team);
            await _bus.RaiseEvent(new TeamCreatedEvent(_dataMapper.Map<TeamDto>(addedTeam)));
        }
    }
}
