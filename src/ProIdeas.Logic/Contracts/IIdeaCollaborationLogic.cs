using System.Collections.Generic;
using ProIdeas.DTO;
using System.Threading.Tasks;

namespace ProIdeas.Logic.Contracts
{
    public interface IIdeaCollaborationLogic
    {
        Task<IEnumerable<IdeaCommentDto>> GetComments(string ideaId);

        Task<IdeaCommentDto> GetComment(string commentId);

        Task<IdeaCollaborationStatsDto> GetStats(string ideaId);


    }
}