using ProIdeas.Domain.Core.Events;

namespace ProIdeas.Infra.Events
{
    public class IdeaUnpublishedEvent : Event
    {
        public string IdeaId { get; private set; }

        public IdeaUnpublishedEvent(string ideaId)
        {
            IdeaId = ideaId;
        }
    }
}
