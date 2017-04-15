using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO;

namespace ProIdeas.Infra.Events
{
    public class IdeaDeletedEvent : Event
    {
        public IdeaDto Idea { get; private set; }

        public IdeaDeletedEvent(IdeaDto idea)
        {
            Idea = idea;
        }
    }
}
