using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProIdeas.Services.Contracts;

namespace ProIdeas.UI.Controllers
{
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
            return View();
        }

        // GET: Idea/Create
        public ActionResult Images(string ideaId)
        {
            //var idea = _ideaService.GetIdea(ideaId);
            return View();
        }

        // POST: Idea/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Idea/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Idea/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Idea/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}