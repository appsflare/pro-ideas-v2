namespace ProIdeas.DTO.Tasks
{
    public class TaskBoardDto : BaseDto<string>
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public string TeamId { get; set; }

        public string IdeaId { get; set; }



    }
}
