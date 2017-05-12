using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO.Tasks;

namespace ProIdeas.Infra.Events.Tasks
{
    public class TaskItemUpdatedEvent : Event
    {
        public TaskItemDto Item { get; private set; }

        public TaskItemUpdatedEvent(TaskItemDto item)
        {
            Item = item;
        }

    }
}
