using ProIdeas.Domain.Core.Commands;
using ProIdeas.DTO.Tasks;

namespace ProIdeas.Infra.Commands.Tasks
{
    public class AddTaskItemStateCommand : Command
    {
        public string BoardId { get; private set; }
        public TaskItemStateDto State { get; set; }

        public AddTaskItemStateCommand(string boardId, TaskItemStateDto state)
        {
            BoardId = boardId;
            State = state;
        }

        public void SetStateId(string id)
        {
            State.Id = id;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(BoardId) && State != null && 
                !string.IsNullOrEmpty(State.Name?.Trim());
        }
    }
}
