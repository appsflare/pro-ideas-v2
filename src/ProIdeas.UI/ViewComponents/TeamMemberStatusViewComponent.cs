using Microsoft.AspNetCore.Mvc;
using ProIdeas.Services;
using ProIdeas.UI.Models.TeamViewModels;
using System.Threading.Tasks;

namespace ProIdeas.UI.ViewComponents
{
    public class TeamMemberStatusViewComponent : ViewComponent
    {
        private readonly ITeamService _teamService;
        public TeamMemberStatusViewComponent(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userId,string ideaId)
        {
            var model = TeamMemberStatusViewModel.MapFrom(await _teamService.GetTeamMemberAsync(userId, ideaId), ideaId, userId);
            return View(model);
        }
    }
}
