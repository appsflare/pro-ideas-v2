using ProIdeas.Domain.Core.Commands;
using ProIdeas.DTO.Tasks;

namespace ProIdeas.Infra.Commands.Tasks
{
    public class UpdateTaskItemTypeCommand : Command
    {

        public TaskItemTypeDto Type { get; set; }

        public UpdateTaskItemTypeCommand(TaskItemTypeDto type)
        {

            Type = type;
        }

        public override bool IsValid()
        {
            return Type != null &&
                !string.IsNullOrEmpty(Type.Name?.Trim());
        }
    }
}
