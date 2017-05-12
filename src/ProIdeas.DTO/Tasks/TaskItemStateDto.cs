namespace ProIdeas.DTO.Tasks
{
    public class TaskItemStateDto : BaseDto<string>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string TaskBoardId { get; set; }
    }
}
