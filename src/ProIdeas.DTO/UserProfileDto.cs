namespace ProIdeas.DTO
{
    public class UserProfileDto : BaseDto<string>
    {

        public int Ideas { get; set; }

        public int Comments { get; set; }

        public int LikedIdeas { get; set; }

        public int Followers { get; set; }

        public int Followees { get; set; }

        public string OwnerId { get; set; }

        public UserDto Owner { get; set; }

    }
}
