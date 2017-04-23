using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProIdeas.Authentication.Contracts;
using ProIdeas.DTO;
using ProIdeas.Files.Contracts;
using ProIdeas.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ProIdeas.UI.Controllers
{
    [Produces("application/json")]
    [Route("api/ideas")]
    [Authorize]
    public class IdeaApiController : Controller
    {

        private readonly IIdeaService _ideaService;
        private readonly IFileStorage _fileStorage;
        private readonly IUserIdentityProvider _userIdentityProvider;
        public IdeaApiController(IIdeaService ideaService, IFileStorage fileStorage, IUserIdentityProvider userIdentityProvider)
        {
            _ideaService = ideaService;
            _fileStorage = fileStorage;
            _userIdentityProvider = userIdentityProvider;
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
            idea.OwnerId = _userIdentityProvider.GetUserId();
            return _ideaService.CreateAsync(idea);
        }

        // PUT: api/IdeaApi/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]IdeaDto idea)
        {
            _ideaService.Update(idea);
            return Json(new { message = "Idea updated successfully" });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {

        }

        [HttpGet]
        [Route("{id}/banner")]
        [AllowAnonymous]
        async public Task<IActionResult> GetBannerImage(string id)
        {
            var stream = await _fileStorage.GetFileStreamAsync($"{id}.png");

            if (stream == Stream.Null)
            {
                return File("~/images/sample-image.png", "image/png");
            }

            return new FileStreamResult(stream, "image/png");
        }

        [HttpPost]
        [Route("{id}/banner")]
        async public Task<IActionResult> SetBannerImage([FromRoute]string id)
        {
            var bannerImage = $"{id}.png";
            var addedFiles = new List<Guid>();
            foreach (var file in Request.Form.Files)
            {
                var uploadedFileId = await _fileStorage.AddFileAsync(bannerImage, file.OpenReadStream(), new { ideaId = id });

                addedFiles.Add(uploadedFileId);
            }

            return Json(addedFiles);
        }

        [HttpGet]
        [Route("{id}/pages")]
        async public Task<IActionResult> GetPages([FromRoute]string id)
        {
            var idea = await _ideaService.GetIdeaAsync(id);

            return Json(idea.Pages);
        }


        [HttpPut]
        [Route("{id}/pages")]
        public IActionResult SavePages([FromRoute]string id, [FromBody]IEnumerable<PageDto> pages)
        {
            _ideaService.SavePages(id, pages);

            return Json(new { message = "Pages saved successfully" });
        }
    }
}
