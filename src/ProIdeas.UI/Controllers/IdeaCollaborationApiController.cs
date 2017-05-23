using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProIdeas.Services.Contracts;
using ProIdeas.DTO;
using ProIdeas.Authentication.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace ProIdeas.UI.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    [Authorize]
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
        public async Task<IdeaCommentDto> CreateComment(string ideaId, [FromBody] IdeaCommentDto comment)
        {
            comment.IdeaId = ideaId;
            comment.OwnerId = _userIdentityProvider.GetUserId();
            var createdComment = await _ideaCommentService.CreateAsync(comment);
            return await _ideaCommentService.GetCommentAsync(createdComment.Id);
        }

        [HttpPut, Route("comments")]
        public async Task<IActionResult> UpdateComment([FromBody] IdeaCommentDto comment)
        {
            comment.OwnerId = _userIdentityProvider.GetUserId();
            await _ideaCommentService.UpdateAsync(comment);
            return Json(new { message = "Comment updated successfully" });
        }


        [HttpDelete, Route("ideas/comments/{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            await _ideaCommentService.DeleteCommentAsync(id);
            return Json(new { message = "Comment deleted successfully" });
        }

        [HttpPut, Route("ideas/{ideaId}/likes/{like}")]
        public async Task<IdeaCollaborationStatsDto> Update(string ideaId, bool like)
        {
            await _ideaCommentService.UpdateAsync(ideaId, _userIdentityProvider.GetUserId(), like);
            return await _ideaCommentService.GetStatsAsync(ideaId);
        }

        [HttpPost, Route("ideas/{ideaId}/team")]
        public async Task<TeamDto> CreateTeam(string ideaId, TeamDto team)
        {
            team.IdeaId = ideaId;
            return await _ideaCommentService.CreateTeamAsync(team);
        }

        [HttpPut, Route("ideas/{ideaId}/team/{teamId}")]
        public async Task<TeamDto> CreateTeam(string ideaId, string teamId, TeamDto team)
        {
            team.IdeaId = ideaId;
            team.Id = teamId;

            return await _ideaCommentService.UpdateTeamAsync(team);
        }


        [HttpGet, Route("users/{userId}/activities")]
        public async Task<IEnumerable<ActivityDto>> GetActivities(string userId)
        {            
            return await _ideaCommentService.GetActivitiesAsync(userId);
        }
    }
}