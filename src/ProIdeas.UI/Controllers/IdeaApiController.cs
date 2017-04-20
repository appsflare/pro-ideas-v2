using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProIdeas.Services.Contracts;
using ProIdeas.DTO;
using System.Threading.Tasks;

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
        [HttpGet("filter", Name = "Filter")]
        public Task<IEnumerable<IdeaDto>> Filter(int pageSize = 10, int page = 1)
        {
            return _ideaService.GetIdeasAsync(pageSize, page);
        }

        // GET: api/IdeaApi/5
        [HttpGet("{id}", Name = "Get")]
        public Task<IdeaDto> Get(string id)
        {
            return _ideaService.GetIdeaAsync(id);
        }

        // POST: api/IdeaApi
        [HttpPost]
        public Task<IdeaDto> Post([FromBody]IdeaDto idea)
        {
            return _ideaService.CreateAsync(idea);
        }

        // PUT: api/IdeaApi/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]IdeaDto idea)
        {
            _ideaService.Update(idea);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
