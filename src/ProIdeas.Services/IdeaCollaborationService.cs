using ProIdeas.Services.Contracts;
using System.Collections.Generic;
using ProIdeas.DTO;
using ProIdeas.Logic.Contracts;
using System.Threading.Tasks;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Infra.Commands.Collaboration;

namespace ProIdeas.Services
{
    public class IdeaCollaborationService : IIdeaCollaborationService
    {
        private readonly IIdeaCollaborationLogic _ideaCollaborationLogic;
        private readonly IBus _bus;
        public IdeaCollaborationService(IIdeaCollaborationLogic ideaCommentLogic, IBus bus)
        {
            _ideaCollaborationLogic = ideaCommentLogic;
            _bus = bus;

        }

        public Task<IdeaCommentDto> CreateAsync(IdeaCommentDto comment)
        {
            var command = new CreateIdeaCommentCommand(comment);

            _bus.SendCommand(command);

            return GetCommentAsync(command.Comment.Id);
        }

        public Task DeleteComment(string commentId)
        {
            var command = new DeleteIdeaCommentCommand(commentId);

            return _bus.SendCommand(command);
        }

        public Task<IdeaCommentDto> GetCommentAsync(string commentId)
        {
            return _ideaCollaborationLogic.GetComment(commentId);
        }

        public Task<IEnumerable<IdeaCommentDto>> GetCommentsAsync(string ideaId)
        {
            return _ideaCollaborationLogic.GetComments(ideaId);
        }

        public Task<IdeaCollaborationStatsDto> GetStats(string ideaId)
        {
            return _ideaCollaborationLogic.GetStats(ideaId);
        }

        public Task Update(IdeaCommentDto comment)
        {
            var command = new UpdateIdeaCommentCommand(comment);

            return _bus.SendCommand(command);
        }

        public Task Update(string ideaId, string userId, bool like)
        {
            return _bus.SendCommand(new LikeIdeaCommand(ideaId, userId, like));
        }

    }
}
