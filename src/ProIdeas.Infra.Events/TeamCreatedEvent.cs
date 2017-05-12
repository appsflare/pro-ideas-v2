using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO;

namespace ProIdeas.Infra.Events
{
    public class TeamCreatedEvent : Event
    {
        public TeamDto Team { get; private set; }

        public TeamCreatedEvent(TeamDto team)
        {
            Team = team;
        }
    }
}
