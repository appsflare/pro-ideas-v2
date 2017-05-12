using ProIdeas.Domain.Core.Commands;
using ProIdeas.DTO.Tasks;

namespace ProIdeas.Infra.Commands.Tasks
{
    public class AddTaskItemCommand : Command
    {
        public string BoardId { get; private set; }
        public TaskItemDto Item { get; set; }

        public AddTaskItemCommand(string boardId, TaskItemDto item)
        {
            BoardId = boardId;
            Item = item;
        }

        public void SetTaskItemId(string id)
        {
            Item.Id = id;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(BoardId) && Item != null && 
                !string.IsNullOrEmpty(Item.Content?.Trim());
        }
    }
}
