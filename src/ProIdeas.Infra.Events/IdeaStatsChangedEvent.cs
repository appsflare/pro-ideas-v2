using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO;

namespace ProIdeas.Infra.Events
{
    public class IdeaStatsChangedEvent : Event
    {
        public IdeaDto Idea { get; private set; }

        public IdeaStatsChangedEvent(IdeaDto idea)
        {
            Idea = idea;
        }
    }
}
