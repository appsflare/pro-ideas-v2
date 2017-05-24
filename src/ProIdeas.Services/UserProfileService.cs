using ProIdeas.DTO;
using ProIdeas.Logic.Contracts;
using ProIdeas.Services.Contracts;
using System.Threading.Tasks;
using System;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Infra.Commands.Users;

namespace ProIdeas.Services
{
    public class UserProfileService : IUserProfileService
    {

        private readonly IUserProfileLogic _userProfileLogic;
        private readonly IBus _bus;

        public UserProfileService(IUserProfileLogic userProfileLogic, IBus bus)
        {
            _userProfileLogic = userProfileLogic;
            _bus = bus;
        }

        private async Task EnsureUserProfile(string userId)
        {
            await _bus.SendCommand(new CreateUserProfileCommand(userId));
        }

        public async Task<UserProfileDto> GetUserProfileAsync(string userId)
        {
            await EnsureUserProfile(userId);

            return await _userProfileLogic.GetUserProfileAsync(userId);
        }
    }
}
