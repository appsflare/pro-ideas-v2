using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO;

namespace ProIdeas.Infra.Events
{
    public class IdeaUpdatedEvent : Event
    {
        public IdeaDto Idea { get; private set; }

        public IdeaUpdatedEvent(IdeaDto idea)
        {
            Idea = idea;
        }
    }
}
