namespace ProIdeas.Domain.Entities
{
    public class UserProfile : BaseEntity
    {

        public int IdeasCount { get; set; }

        public int CommentsCount { get; set; }

        public int LikesCount { get; set; }

        public int DisLikesCount { get; set; }

        public int FollowersCount { get; set; }

        public int FolloweesCount { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

    }
}
