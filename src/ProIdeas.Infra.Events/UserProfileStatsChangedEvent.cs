using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO;

namespace ProIdeas.Infra.Events
{
    public class UserProfileStatsChangedEvent : Event
    {
        public UserProfileStatsDto Stats { get; private set; }

        public UserProfileStatsChangedEvent(UserProfileStatsDto idea)
        {
            Stats = idea;
        }
    }
}
