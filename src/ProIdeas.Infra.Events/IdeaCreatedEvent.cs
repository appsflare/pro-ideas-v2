using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO;

namespace ProIdeas.Infra.Events
{
    public class IdeaCreatedEvent : Event
    {
        public IdeaDto Idea { get; private set; }

        public IdeaCreatedEvent(IdeaDto idea)
        {
            Idea = idea;
        }
    }
}
