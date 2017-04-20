using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProIdeas.Services.Contracts;
using ProIdeas.UI.Models.IdeaViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ProIdeas.UI.Controllers
{
    [Authorize]
    public class IdeaController : Controller
    {
        private readonly IIdeaService _ideaService;
        public IdeaController(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        // GET: Idea
        public ActionResult Index()
        {
            return View();
        }

        // GET: Idea/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Idea/Create
        public ActionResult Create()
        {
            return View(new IdeaInfoViewModel());
        }

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

        // GET: Idea/Images
        async public Task<IActionResult> Images(string id)
        {
            var idea = await _ideaService.GetIdeaAsync(id);

            return View(GetIdeaInfoViewModel(idea));
        }

        // GET: Idea/Pages
        async public Task<IActionResult> Pages(string id)
        {
            var idea = await _ideaService.GetIdeaAsync(id);

            return View(GetIdeaInfoViewModel(idea));
        }
    
       

        // GET: Idea/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}