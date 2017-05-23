using ProIdeas.DTO;

namespace ProIdeas.UI.Models.User
{
    public class UserProfileViewModel
    {
        private static int GetInnovationPoints(UserProfileDto model)
        {
            return model.CommentsCount + (model.IdeasCount * 10);
        }

        public int Ideas { get; set; }

        public int Comments { get; set; }

        public int LikedIdeas { get; set; }

        public int Followers { get; set; }

        public int Followees { get; set; }

        public string OwnerId { get; set; }

        public string FullName { get; set; }

        public int InnovationPoints { get; set; }

        public static UserProfileViewModel MapFrom(UserProfileDto model)
        {
            return new UserProfileViewModel
            {
                Comments = model.CommentsCount,
                Followees = model.FolloweesCount,
                Followers = model.FollowersCount,
                FullName = model.Owner.FullName,
                Ideas = model.IdeasCount,
                OwnerId = model.OwnerId,
                LikedIdeas = model.LikesCount,
                InnovationPoints = GetInnovationPoints(model)
            };
        }
    }
}
