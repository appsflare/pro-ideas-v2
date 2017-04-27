using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProIdeas.Services.Contracts;
using ProIdeas.DTO;
using ProIdeas.Authentication.Contracts;

namespace ProIdeas.UI.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class IdeaCollaborationApiController : Controller
    {
        private readonly IIdeaCollaborationService _ideaCommentService;
        private readonly IUserIdentityProvider _userIdentityProvider;
        public IdeaCollaborationApiController(IIdeaCollaborationService ideaCommentService, IUserIdentityProvider userIdentityProvider)
        {
            _ideaCommentService = ideaCommentService;
            _userIdentityProvider = userIdentityProvider;
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
        public IActionResult UpdateComment(string ideaId, [FromBody] IdeaCommentDto comment)
        {
            comment.UserId = _userIdentityProvider.GetUserId();
            _ideaCommentService.Update(comment);
            return Json(new { message = "Comment updated successfully" });
        }

        [HttpPut, Route("ideas/{ideaId}/likes/{like}")]
        public Task<IdeaCollaborationStatsDto> Update(string ideaId, bool like)
        {
            _ideaCommentService.Update(ideaId, _userIdentityProvider.GetUserId(), like);
            return _ideaCommentService.GetStats(ideaId);
        }
    }
}