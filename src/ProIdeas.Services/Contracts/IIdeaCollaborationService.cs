using ProIdeas.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Services.Contracts
{
    public interface IIdeaCollaborationService
    {

        Task<IEnumerable<ActivityDto>> GetActivitiesAsync(string userId);

        Task<IEnumerable<ActivityDto>> GetContributionsAsync(string userId);

        Task<IdeaCollaborationStatsDto> GetStatsAsync(string ideaId);

        Task<IdeaCommentDto> GetCommentAsync(string commentId);

        Task<IEnumerable<IdeaCommentDto>> GetCommentsAsync(string ideaId);

        Task<IdeaCommentDto> CreateAsync(IdeaCommentDto comment);

        Task UpdateAsync(IdeaCommentDto comment);

        Task DeleteCommentAsync(string commentId);

        Task UpdateAsync(string ideaId, string userId, bool like);

        Task<TeamDto> GetTeamAsync(string ideaId);

        Task<TeamDto> CreateTeamAsync(TeamDto team);

        Task<TeamDto> UpdateTeamAsync(TeamDto team);        

    }
}
