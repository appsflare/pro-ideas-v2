using ProIdeas.Domain.Core.Commands;

namespace ProIdeas.Infra.Commands.Tasks
{
    public class DeleteTaskItemCommand : Command
    {
        public string TaskItemId { get; set; }

        public DeleteTaskItemCommand(string taskItemId)
        {
            TaskItemId = taskItemId;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(TaskItemId?.Trim());
        }
    }
}
