using ProIdeas.Domain.Core.Commands;
using ProIdeas.DTO;

namespace ProIdeas.Infra.Commands.Collaboration
{
    public class UpdateIdeaCommentCommand : Command
    {
        public IdeaCommentDto Comment { get; private set; }

        public UpdateIdeaCommentCommand(IdeaCommentDto comment)
        {
            Comment= comment;
        }

        public override bool IsValid()
        {
            return Comment != null
                && !string.IsNullOrEmpty(Comment.Content?.Trim())
                && !string.IsNullOrEmpty(Comment.OwnerId?.Trim());
        }
    }
}
