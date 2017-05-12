using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO.Tasks;

namespace ProIdeas.Infra.Events.Tasks
{
    public class TaskItemStateUpdatedEvent : Event
    {
        public TaskItemStateDto State { get; private set; }

        public TaskItemStateUpdatedEvent(TaskItemStateDto state)
        {
            State = state;
        }

    }
}
