using ProIdeas.Domain.Core.Events;

namespace ProIdeas.Infra.Events
{
    public class IdeaPublishedEvent : Event
    {
        public string IdeaId { get; private set; }

        public IdeaPublishedEvent(string ideaId)
        {
            IdeaId = ideaId;
        }
    }
}
