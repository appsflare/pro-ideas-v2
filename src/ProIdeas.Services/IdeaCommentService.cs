using ProIdeas.Services.Contracts;
using System.Collections.Generic;
using ProIdeas.DTO;
using ProIdeas.Logic.Contracts;
using System.Threading.Tasks;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Infra.Commands.Comments;

namespace ProIdeas.Services
{
    public class IdeaCommentService : IIdeaCommentService
    {
        private readonly IIdeaCommentLogic _ideaCommentLogic;
        private readonly IBus _bus;
        public IdeaCommentService(IIdeaCommentLogic ideaCommentLogic, IBus bus)
        {
            _ideaCommentLogic = ideaCommentLogic;
            _bus = bus;

        }

        public Task<IdeaCommentDto> CreateAsync(IdeaCommentDto comment)
        {
            var command = new CreateIdeaCommentCommand(comment);

            _bus.SendCommand(command);

            return GetCommentAsync(command.Comment.Id);
        }

        public Task<IdeaCommentDto> GetCommentAsync(string commentId)
        {
            return _ideaCommentLogic.GetComment(commentId);
        }

        public Task<IEnumerable<IdeaCommentDto>> GetCommentsAsync(string ideaId)
        {
            return _ideaCommentLogic.GetComments(ideaId);
        }

        public void Update(IdeaCommentDto comment)
        {
            var command = new UpdateIdeaCommentCommand(comment);

            _bus.SendCommand(command);
        }
    }
}
