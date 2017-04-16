using Microsoft.AspNetCore.Mvc;
using ProIdeas.Services.Contracts;
using ProIdeas.UI.Models.IdeaViewModels;

namespace ProIdeas.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdeaService _ideaService;

        public HomeController(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }


        public IActionResult Index()
        {
            var ideas = _ideaService.GetIdeas(10, 1);



            return View(new IndexIdeasViewModel
            {
                Ideas = ideas
            });
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
