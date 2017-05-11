namespace ProIdeas.DTO
{
    public class UserProfileStatsDto
    {
        public string UserId { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
        public int Comments { get; set; }

        public int Ideas { get; set; }
    }
}
