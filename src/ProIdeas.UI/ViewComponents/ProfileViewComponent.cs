using Microsoft.AspNetCore.Mvc;
using ProIdeas.Services.Contracts;
using ProIdeas.UI.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.UI.ViewComponents
{
    public class ProfileViewComponent : ViewComponent
    {
        private readonly IUserProfileService _userProfileService;
        public ProfileViewComponent(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            var model = UserProfileViewModel.MapFrom(await _userProfileService.GetUserProfileAsync(userId));
            return View(model);
        }
    }
}
