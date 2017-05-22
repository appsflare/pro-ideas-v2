using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProIdeas.Services.Contracts;
using ProIdeas.UI.Models.IdeaViewModels;
using System.Threading.Tasks;
using System.Linq;
using ProIdeas.Services;

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

        [HttpPost, Route("{id}/{userId}/register")]
        async public Task<IActionResult> Register(string id, string userId)
        {
            await _teamService.RequestToJoinTeamAsync(userId, id);
            return GoToIdeaDetails(id);
        }

        [HttpPost, Route("{id}/{userId}/approve")]
        async public Task<IActionResult> Approve(string id,string userId)
        {
            await _teamService.ApproveJoinRequestAsync(userId, id);
            return GoToIdeaDetails(id);
        }

        [HttpPost, Route("{id}/{userId}/reject")]
        async public Task<IActionResult> Reject(string id,string userId)
        {
            await _teamService.RejectJoinRequestAsync(userId, id);
            return GoToIdeaDetails(id);
        }
    }
}