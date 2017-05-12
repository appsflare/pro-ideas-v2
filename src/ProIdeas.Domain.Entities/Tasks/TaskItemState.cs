namespace ProIdeas.Domain.Entities.Tasks
{
    public class TaskItemState : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string TaskBoardId { get; set; }
    }
}
