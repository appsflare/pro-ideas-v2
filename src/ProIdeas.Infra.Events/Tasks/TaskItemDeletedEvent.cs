using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO.Tasks;

namespace ProIdeas.Infra.Events.Tasks
{
    public class TaskItemDeletedEvent : Event
    {
        public TaskItemDto Item { get; private set; }

        public TaskItemDeletedEvent(TaskItemDto item)
        {
            Item = item;
        }

    }
}
