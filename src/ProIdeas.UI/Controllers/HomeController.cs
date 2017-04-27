using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProIdeas.Authentication.Contracts;
using ProIdeas.DTO;
using ProIdeas.Services.Contracts;
using ProIdeas.UI.Models.IdeaViewModels;
using System.Linq;
using System.Threading.Tasks;


namespace ProIdeas.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IIdeaService _ideaService;
        private readonly IUserIdentityProvider _userIdentityProvider;

        public HomeController(IIdeaService ideaService, IUserIdentityProvider userIdentityProvider)
        {
            _ideaService = ideaService;
            _userIdentityProvider = userIdentityProvider;
        }


        public IActionResult Index()
        {
            return View(IndexIdeasViewModel.MapFrom(Enumerable.Empty<IdeaDto>()));
        }

        async public Task<IActionResult> MyIdeas()
        {
            var ideas = await _ideaService.GetUserIdeasAsync(_userIdentityProvider.GetUserId(), 10, 1, string.Empty);

            return View(IndexIdeasViewModel.MapFrom(ideas));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
