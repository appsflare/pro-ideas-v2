using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProIdeas.Services.Contracts;
using System.Threading.Tasks;
using ProIdeas.UI.Models.User;

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
        [Route("profile/:id")]
        public async Task<IActionResult> Details(string id)
        {
          var model = UserActivityViewModel.MapFrom(
              await _collaborationService.GetActivitiesAsync(id),
              await _collaborationService.GetContributionsAsync(id), id);
          return View(model);
        }
    }
}
