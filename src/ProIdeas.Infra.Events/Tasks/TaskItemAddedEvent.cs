using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO.Tasks;

namespace ProIdeas.Infra.Events.Tasks
{
    public class TaskItemAddedEvent : Event
    {
        public TaskItemDto Item { get; private set; }

        public TaskItemAddedEvent(TaskItemDto item)
        {
            Item = item;
        }

    }
}
