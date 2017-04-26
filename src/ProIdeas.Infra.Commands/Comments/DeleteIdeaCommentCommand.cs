using ProIdeas.Domain.Core.Commands;

namespace ProIdeas.Infra.Commands.Comments
{
    public class DeleteIdeaCommentCommand : Command
    {
        public string CommentId { get; private set; }

        public DeleteIdeaCommentCommand(string commentId)
        {
            CommentId = commentId;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(CommentId?.Trim());
        }
    }
}
