using ProIdeas.Domain.Core.Commands;
using ProIdeas.DTO;

namespace ProIdeas.Infra.Commands.Comments
{
    public class CreateIdeaCommentCommand : Command
    {
        public IdeaCommentDto Comment { get; private set; }

        public CreateIdeaCommentCommand(IdeaCommentDto comment)
        {
            Comment= comment;
        }

        public void SetCommentId(string id)
        {
            Comment.Id = id;
        }

        public override bool IsValid()
        {
            return Comment != null
                && !string.IsNullOrEmpty(Comment.Content?.Trim())
                && !string.IsNullOrEmpty(Comment.UserId.Trim());
        }
    }
}
