using ProIdeas.Domain.Core.Commands;
using ProIdeas.DTO.Tasks;

namespace ProIdeas.Infra.Commands.Tasks
{
    public class CreateTaskBoardCommand : Command
    {
        public string IdeaId { get; private set; }
        public TaskBoardDto TaskBoard { get; set; }

        public CreateTaskBoardCommand(string ideaId, TaskBoardDto taskBoard)
        {
            IdeaId = ideaId;
            TaskBoard = taskBoard;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(IdeaId) && TaskBoard != null && 
                !string.IsNullOrEmpty(TaskBoard.Title?.Trim()) &&
                !string.IsNullOrEmpty(TaskBoard.TeamId?.Trim());
        }
    }
}
