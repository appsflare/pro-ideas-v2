using ProIdeas.DTO;
using System.Threading.Tasks;

namespace ProIdeas.Services.Contracts
{
    public interface IUserProfileService
    {
        Task<UserProfileDto> GetUserProfileAsync(string userId);
    }
}
