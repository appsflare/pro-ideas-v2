using ProIdeas.Domain.Core.Commands;

namespace ProIdeas.Infra.Commands.Collaboration
{
    public class LikeIdeaCommand : Command
    {
        public string IdeaId { get; private set; }

        public string UserId { get; private set; }

        public bool Like { get; set; }


        public LikeIdeaCommand(string ideaId, string userId, bool like)
        {
            IdeaId = ideaId;
            UserId = userId;
            Like = like;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(IdeaId?.Trim()) && !string.IsNullOrEmpty(UserId.Trim());
        }
    }
}
