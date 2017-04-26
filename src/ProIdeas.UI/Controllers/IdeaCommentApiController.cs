using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProIdeas.Services.Contracts;
using ProIdeas.DTO;

namespace ProIdeas.UI.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class IdeaCommentApiController : Controller
    {
        private readonly IIdeaCommentService _ideaCommentService;
        public IdeaCommentApiController(IIdeaCommentService ideaCommentService)
        {
            _ideaCommentService = ideaCommentService;
        }

        [HttpGet, Route("comments/{id}")]
        public Task<IdeaCommentDto> GetComment(string id)
        {
            return _ideaCommentService.GetCommentAsync(id);
        }

        [HttpGet, Route("ideas/{ideaId}/comments")]
        public Task<IEnumerable<IdeaCommentDto>> GetComments(string ideaId)
        {
            return _ideaCommentService.GetCommentsAsync(ideaId);
        }

        [HttpPost, Route("ideas/{ideaId}/comments")]
        public Task<IdeaCommentDto> CreateComment(string ideaId, [FromBody] IdeaCommentDto comment)
        {
            return _ideaCommentService.CreateAsync(comment);
        }

        [HttpPut, Route("ideas/{ideaId}/comments")]
        public IActionResult  UpdateComment(string ideaId, [FromBody] IdeaCommentDto comment)
        {
             _ideaCommentService.Update(comment);
            return Json(new { message = "Comment updated successfully" });
        }
    }
}