namespace ProIdeas.Domain.Entities.Tasks
{
    public class TaskBoard : BaseEntity
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public string TeamId { get; set; }

        public string IdeaId { get; set; }
        


    }
}
