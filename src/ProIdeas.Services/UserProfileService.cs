using ProIdeas.DTO;
using ProIdeas.Logic.Contracts;
using ProIdeas.Services.Contracts;
using System.Threading.Tasks;

namespace ProIdeas.Services
{
    public class UserProfileService : IUserProfileService
    {

        private readonly IUserProfileLogic _userProfileLogic;

        public UserProfileService(IUserProfileLogic userProfileLogic)
        {
            _userProfileLogic = userProfileLogic;
        }

        public Task<UserProfileDto> GetUserProfileAsync(string userId)
        {
            return _userProfileLogic.GetUserProfileAsync(userId);            
        }
    }
}
