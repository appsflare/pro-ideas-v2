namespace ProIdeas.Domain.Entities
{
    public class UserProfile : BaseEntity
    {

        public int Ideas { get; set; }

        public int Comments { get; set; }

        public int LikedIdeas { get; set; }

        public int Followers { get; set; }

        public int Followees { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

    }
}
