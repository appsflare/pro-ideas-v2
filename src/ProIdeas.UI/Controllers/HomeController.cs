using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProIdeas.Authentication.Contracts;
using ProIdeas.Services.Contracts;
using ProIdeas.UI.Models.IdeaViewModels;
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


        async public Task<IActionResult> Index()
        {
            var ideas = await _ideaService.GetIdeasAsync(10, 1);



            return View(IndexIdeasViewModel.MapFrom(ideas));
        }

        async public Task<IActionResult> MyIdeas()
        {
            var ideas = await _ideaService.GetUserIdeas(_userIdentityProvider.GetUserId(), 10, 1, string.Empty);

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
