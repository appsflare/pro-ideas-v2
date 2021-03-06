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
        [HttpGet("")]
        public Task<IEnumerable<IdeaDto>> Get(int pageSize = 10, int page = 1)
        {
            return _ideaService.GetIdeasAsync(pageSize, page);
        }

        // GET: api/IdeaApi
        [HttpGet("search")]
        public Task<IEnumerable<IdeaDto>> Filter(string keyword, int pageSize = 10, int page = 1)
        {
            return _ideaService.SearchIdeasAsync(keyword, pageSize, page);
        }


        [HttpGet("searchmyideas")]
        public Task<IEnumerable<IdeaDto>> FilterMyIdeas(string keyword, int pageSize = 10, int page = 1)
        {
            return _ideaService.GetUserIdeasAsync(_userIdentityProvider.GetUserId(), pageSize, page, keyword);
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
        public async Task<IActionResult> Put(string id, [FromBody]IdeaDto idea)
        {
            await _ideaService.UpdateAsync(idea);
            return Json(new { message = "Idea updated successfully" });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {

        }

        [HttpGet]
        [Route("{id}/banner.png")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBannerImage(string id)
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
        public async Task<IActionResult> SetBannerImage([FromRoute]string id)
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
        public async Task<IActionResult> GetPages([FromRoute]string id)
        {
            var idea = await _ideaService.GetIdeaAsync(id);

            return Json(idea.Pages);
        }


        [HttpPut]
        [Route("{id}/pages")]
        public async Task<IActionResult> SavePages([FromRoute]string id, [FromBody]IEnumerable<PageDto> pages)
        {
            await _ideaService.SavePagesAsync(id, pages);

            return Json(new { message = "Pages saved successfully" });
        }
    }
}
