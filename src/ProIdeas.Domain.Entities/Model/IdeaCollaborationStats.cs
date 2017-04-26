namespace ProIdeas.Domain.Entities.Model
{
    public class IdeaCollaborationStats
    {
        public string IdeaId { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
        public int Comments { get; set; }
    }
}
