using ProIdeas.Domain.Core.Events;

namespace ProIdeas.Infra.Events
{
    public class IdeaLikeChangedEvent : Event
    {
        public string IdeaId { get; private set; }

        public string UserId { get; private set; }

        public bool Like { get; private set; }

        public IdeaLikeChangedEvent(string ideaId, string userId, bool like)
        {
            IdeaId = ideaId;
            UserId = userId;
            Like = like;
        }
    }
}
