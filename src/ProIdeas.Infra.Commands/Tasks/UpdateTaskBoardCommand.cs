using ProIdeas.Domain.Core.Commands;
using ProIdeas.DTO.Tasks;

namespace ProIdeas.Infra.Commands.Tasks
{
    public class UpdateTaskBoardCommand : Command
    {
        
        public TaskBoardDto TaskBoard { get; set; }

        public UpdateTaskBoardCommand(TaskBoardDto taskBoard)
        {            
            TaskBoard = taskBoard;
        }

        public override bool IsValid()
        {
            return TaskBoard != null && 
                !string.IsNullOrEmpty(TaskBoard.Title?.Trim()) &&
                !string.IsNullOrEmpty(TaskBoard.TeamId?.Trim());
        }
    }
}
