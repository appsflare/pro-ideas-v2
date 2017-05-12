using ProIdeas.Domain.Core.Commands;
using ProIdeas.DTO.Tasks;

namespace ProIdeas.Infra.Commands.Tasks
{
    public class UpdateTaskItemCommand : Command
    {
        public TaskItemDto Item { get; set; }

        public UpdateTaskItemCommand(TaskItemDto item)
        {
            Item = item;
        }

        public override bool IsValid()
        {
            return Item != null &&
                !string.IsNullOrEmpty(Item.Content?.Trim());
        }
    }
}
