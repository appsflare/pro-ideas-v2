using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProIdeas.Services.Contracts;
using System.Threading.Tasks;
using ProIdeas.UI.Models.TimeLineViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProIdeas.UI.Controllers
{
    [Authorize]    
    public class UserController : Controller
    {
        private readonly IIdeaCollaborationService _collaborationService;
        public UserController(IIdeaCollaborationService collaborationService)
        {
            _collaborationService = collaborationService;
        }
        // GET: /<controller>/
        [Route("profile/:userId")]
        async public Task<IActionResult> Details(string userId)
        {
           userId = "560e2e37-1056-4b46-90ba-389b8309907d";
           var activities = await _collaborationService.GetActivitiesAsync(userId);
            TimeLineViewModel.MapFrom(activities);
            return View();
        }
    }
}
