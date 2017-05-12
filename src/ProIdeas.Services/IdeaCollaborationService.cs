using ProIdeas.Services.Contracts;
using System.Collections.Generic;
using ProIdeas.DTO;
using ProIdeas.Logic.Contracts;
using System.Threading.Tasks;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Infra.Commands.Collaboration;
using System;

namespace ProIdeas.Services
{
    public class IdeaCollaborationService : IIdeaCollaborationService
    {
        private readonly IIdeaCollaborationLogic _ideaCollaborationLogic;
        private readonly IActivityLogic _activityLogic;
        private readonly IBus _bus;
        public IdeaCollaborationService(IIdeaCollaborationLogic ideaCommentLogic, IBus bus, IActivityLogic activityLogic)
        {
            _ideaCollaborationLogic = ideaCommentLogic;
            _bus = bus;
            _activityLogic = activityLogic;
        }

        public Task<IdeaCommentDto> CreateAsync(IdeaCommentDto comment)
        {
            var command = new CreateIdeaCommentCommand(comment);

            _bus.SendCommand(command);

            return GetCommentAsync(command.Comment.Id);
        }

        async public Task<TeamDto> CreateTeamAsync(TeamDto team)
        {
            await _bus.SendCommand(new CreateTeamCommand(team));

            return await _ideaCollaborationLogic.GetTeamAsync(team.IdeaId);
        }

        public Task DeleteCommentAsync(string commentId)
        {
            var command = new DeleteIdeaCommentCommand(commentId);

            return _bus.SendCommand(command);
        }

        public Task<IEnumerable<ActivityDto>> GetActivitiesAsync(string userId)
        {
            return _activityLogic.GetActivitiesAsync(userId);
        }

        public Task<IdeaCommentDto> GetCommentAsync(string commentId)
        {
            return _ideaCollaborationLogic.GetCommentAsync(commentId);
        }

        public Task<IEnumerable<IdeaCommentDto>> GetCommentsAsync(string ideaId)
        {
            return _ideaCollaborationLogic.GetCommentsAsync(ideaId);
        }

        public Task<IEnumerable<ActivityDto>> GetContributionsAsync(string userId)
        {
            return _activityLogic.GetContributionsAsync(userId);
        }

        public Task<IdeaCollaborationStatsDto> GetStatsAsync(string ideaId)
        {
            return _ideaCollaborationLogic.GetStatsAsync(ideaId);
        }

        public Task<TeamDto> GetTeamAsync(string ideaId)
        {
            return _ideaCollaborationLogic.GetTeamAsync(ideaId);
        }

        public Task UpdateAsync(IdeaCommentDto comment)
        {
            var command = new UpdateIdeaCommentCommand(comment);

            return _bus.SendCommand(command);
        }

        public Task UpdateAsync(string ideaId, string userId, bool like)
        {
            return _bus.SendCommand(new LikeIdeaCommand(ideaId, userId, like));
        }

        public Task<TeamDto> UpdateTeamAsync(TeamDto team)
        {
            throw new NotImplementedException();
        }
    }
}
