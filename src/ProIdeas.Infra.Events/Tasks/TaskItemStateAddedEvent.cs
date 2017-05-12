using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO.Tasks;

namespace ProIdeas.Infra.Events.Tasks
{
    public class TaskItemStateAddedEvent : Event
    {
        public TaskItemStateDto State { get; private set; }

        public TaskItemStateAddedEvent(TaskItemStateDto state)
        {
            State = state;
        }

    }
}
