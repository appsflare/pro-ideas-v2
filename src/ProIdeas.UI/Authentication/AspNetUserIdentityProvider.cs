using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProIdeas.Authentication.Contracts;
using ProIdeas.UI.Models;

namespace ProIdeas.UI.Authentication
{
    internal class AspNetUserIdentityProvider : IUserIdentityProvider
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AspNetUserIdentityProvider(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            
        }

        public string GetUserId()
        {
            return _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
        }
    }
}
