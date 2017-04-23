using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProIdeas.Services.Contracts;
using ProIdeas.UI.Models.IdeaViewModels;
using System.Threading.Tasks;

namespace ProIdeas.UI.Controllers
{
    [Authorize]
    [Route("ideas")]
    public class IdeaController : Controller
    {
        private readonly IIdeaService _ideaService;
        public IdeaController(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        // GET: Idea/Details/5
        [HttpGet, Route("{id}/details")]
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet, Route("create")]
        // GET: Idea/Create
        public ActionResult Create()
        {
            return View(new IdeaInfoViewModel());
        }

        [HttpGet, Route("{id}/edit")]
        // GET: Idea/Edit
        async public Task<IActionResult> Edit(string id)
        {
            var idea = await _ideaService.GetIdeaAsync(id);

            return View(GetIdeaInfoViewModel(idea));
        }

        private static IdeaInfoViewModel GetIdeaInfoViewModel(DTO.IdeaDto idea)
        {
            return new IdeaInfoViewModel
            {
                Id = idea.Id,
                Title = idea.Title,
                Description = idea.Description
            };
        }

        [HttpGet, Route("{id}/images")]
        // GET: Idea/Images
        async public Task<IActionResult> Images(string id)
        {
            var idea = await _ideaService.GetIdeaAsync(id);

            return View(GetIdeaInfoViewModel(idea));
        }

        [HttpGet, Route("{id}/pages")]
        // GET: Idea/Pages
        async public Task<IActionResult> Pages(string id)
        {
            var idea = await _ideaService.GetIdeaAsync(id);

            return View(GetIdeaInfoViewModel(idea));
        }

        [HttpGet, Route("index")]
        public IActionResult Index()
        {
            return View("Search", new IdeaSearchViewModel { Keyword = string.Empty });
        }

        [HttpGet, Route("search/{query}")]
        public IActionResult Search(string query)
        {
            return View("Search", new IdeaSearchViewModel { Keyword = query });
        }

    }
}