using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO.Tasks;

namespace ProIdeas.Infra.Events.Tasks
{
    public class TaskItemTypeUpdatedEvent : Event
    {
        public TaskItemTypeDto Type { get; private set; }

        public TaskItemTypeUpdatedEvent(TaskItemTypeDto type)
        {
            Type = type;
        }

    }
}
