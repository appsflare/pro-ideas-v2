using ProIdeas.Domain.Core.Commands;
using ProIdeas.DTO.Tasks;

namespace ProIdeas.Infra.Commands.Tasks
{
    public class UpdateTaskItemStateCommand : Command
    {
        public TaskItemStateDto State { get; private set; }

        public UpdateTaskItemStateCommand(TaskItemStateDto state)
        {
            State = state;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(State.Name?.Trim());
        }
    }
}
