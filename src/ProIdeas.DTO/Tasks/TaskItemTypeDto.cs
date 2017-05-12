namespace ProIdeas.DTO.Tasks
{
    public class TaskItemTypeDto : BaseDto<TaskItemTypeDto>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string TaskBoardId { get; set; }
    }
}
