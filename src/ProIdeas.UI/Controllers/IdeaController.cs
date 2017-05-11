using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProIdeas.Services.Contracts;
using ProIdeas.UI.Models.IdeaViewModels;
using System.Threading.Tasks;
using System.Linq;

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
        async public Task<IActionResult> Details(string id)
        {
            var idea = await _ideaService.GetIdeaAsync(id);

            var model = IdeaInfoViewModel.MapFrom(idea);
            if (model.Pages.Any())
            {
                model.Pages.First().IsActive = true;
            }

            return View(model);
        }

        [HttpGet, Route("create")]
        // GET: Idea/Create
        public IActionResult Create()
        {
            return View(IdeaInfoViewModel.CreateEmpty());
        }

        [HttpGet, Route("{id}/edit")]
        // GET: Idea/Edit
        async public Task<IActionResult> Edit(string id)
        {
            var idea = await _ideaService.GetIdeaAsync(id);

            return View(IdeaInfoViewModel.MapFrom(idea));
        }


        [HttpGet, Route("{id}/images")]
        // GET: Idea/Images
        async public Task<IActionResult> Images(string id)
        {
            var idea = await _ideaService.GetIdeaAsync(id);

            return View(IdeaInfoViewModel.MapFrom(idea));
        }

        [HttpGet, Route("{id}/pages")]
        // GET: Idea/Pages
        async public Task<IActionResult> Pages(string id)
        {
            var idea = await _ideaService.GetIdeaAsync(id);

            return View(IdeaInfoViewModel.MapFrom(idea));
        }

        [HttpGet, Route("index")]
        public IActionResult Index()
        {
            return View("Search", IdeaSearchViewModel.CreateWithKeyword(string.Empty));
        }

        [HttpGet, Route("search/{query}")]
        public IActionResult Search(string query)
        {
            return View("Search", IdeaSearchViewModel.CreateWithKeyword(query));
        }

        [HttpPost, Route("{id}/publish")]
        public IActionResult Publish(string id)
        {
            _ideaService.PublishAsync(id);
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost, Route("{id}/unpublish")]
        public IActionResult Unpublish(string id)
        {
            _ideaService.UnpublishAsync(id);
            return RedirectToAction(nameof(Details), new { id });
        }

    }
}