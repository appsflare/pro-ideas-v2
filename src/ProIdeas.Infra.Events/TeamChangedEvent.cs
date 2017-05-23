using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO;

namespace ProIdeas.Infra.Events
{
    public class TeamChangedEvent : Event
    {
        public TeamDto Team { get; private set; }

        public TeamChangedEvent(TeamDto team)
        {
            Team = team;
        }
    }
}
