using System.Collections.Generic;
using ProIdeas.DTO;
using System.Threading.Tasks;

namespace ProIdeas.Logic.Contracts
{
    public interface IIdeaCollaborationLogic
    {
        Task<IEnumerable<IdeaCommentDto>> GetCommentsAsync(string ideaId);

        Task<IdeaCommentDto> GetCommentAsync(string commentId);

        Task<IdeaCollaborationStatsDto> GetStatsAsync(string ideaId);

        Task<TeamDto> GetTeamAsync(string ideaId);

    }
}