using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProIdeas.Services.Contracts;
using ProIdeas.DTO;

namespace ProIdeas.UI.Controllers
{
    [Produces("application/json")]
    [Route("api/ideas")]
    public class IdeaApiController : Controller
    {

        private readonly IIdeaService _ideaService;
        public IdeaApiController(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        // GET: api/IdeaApi
        [HttpGet]
        public IEnumerable<IdeaDto> Get(int pageSize = 10, int page = 1)
        {
            return _ideaService.GetIdeas(pageSize, page);
        }

        // GET: api/IdeaApi/5
        [HttpGet("{id}", Name = "Get")]
        public IdeaDto Get(string id)
        {
            return _ideaService.GetIdea(id);
        }
        
        // POST: api/IdeaApi
        [HttpPost]
        public void Post([FromBody]IdeaDto idea)
        {
            

        }
        
        // PUT: api/IdeaApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]IdeaDto idea)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
