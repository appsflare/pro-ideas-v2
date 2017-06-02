using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ProIdeas.Services;
using ProIdeas.UI.Models.TeamViewModels;

namespace ProIdeas.UI.Controllers
{
    [Authorize]
    [Route("team")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        private IActionResult GoToIdeaDetails(string id)
        {
            return RedirectToAction(nameof(IdeaController.Details), "Idea", new { id });
        }

        private IActionResult GoToTeamDetails(string id)
        {
            return RedirectToAction(nameof(IdeaController.Teamdetails), "Idea", new { id });
        }

        [HttpGet, Route("{id}/details")]
        public async Task<IActionResult> Details(string id)
        {
            var model = TeamDetailsViewModel.MapFrom(await _teamService.GetTeamAsync(id));
            return View(model);
        }

        [HttpPost, Route("{id}/{userId}/register")]
        public async Task<IActionResult> Register(string id, string userId)
        {
            await _teamService.RequestToJoinTeamAsync(userId, id);
            return GoToIdeaDetails(id);
        }

        [HttpPost, Route("{id}/{userId}/approve")]
        public async Task<IActionResult> Approve(string id,string userId)
        {
            await _teamService.ApproveJoinRequestAsync(userId, id);
            return GoToTeamDetails(id);
        }

        [HttpPost, Route("{id}/{userId}/reject")]
        public async Task<IActionResult> Reject(string id,string userId)
        {
            await _teamService.RejectJoinRequestAsync(userId, id);
            return GoToTeamDetails(id);
        }
    }
}