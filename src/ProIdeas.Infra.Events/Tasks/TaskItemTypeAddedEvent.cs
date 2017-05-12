using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO.Tasks;

namespace ProIdeas.Infra.Events.Tasks
{
    public class TaskItemTypeAddedEvent : Event
    {
        public TaskItemTypeDto Type { get; private set; }

        public TaskItemTypeAddedEvent(TaskItemTypeDto type)
        {
            Type = type;
        }

    }
}
