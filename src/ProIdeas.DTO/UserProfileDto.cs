namespace ProIdeas.DTO
{
    public class UserProfileDto : BaseDto<string>
    {

        public int IdeasCount { get; set; }

        public int CommentsCount { get; set; }

        public int LikesCount { get; set; }

        public int FollowersCount { get; set; }

        public int FolloweesCount { get; set; }

        public string OwnerId { get; set; }

        public UserDto Owner { get; set; }

    }
}
