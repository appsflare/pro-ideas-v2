using ProIdeas.Domain.Core.Commands;
using ProIdeas.DTO.Tasks;

namespace ProIdeas.Infra.Commands.Tasks
{
    public class AddTaskItemTypeCommand : Command
    {
        public string BoardId { get; private set; }
        public TaskItemTypeDto Type { get; set; }

        public AddTaskItemTypeCommand(string boardId, TaskItemTypeDto type)
        {
            BoardId = boardId;
            Type = type;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(BoardId) && Type != null && 
                !string.IsNullOrEmpty(Type.Name?.Trim());
        }

        public void SetStateId(string id)
        {
            Type.Id = id;
        }
    }
}
