using System.Collections.Generic;
using ProIdeas.DTO;
using System.Threading.Tasks;

namespace ProIdeas.Logic.Contracts
{
    public interface IIdeaLogic
    {
        Task<IEnumerable<IdeaLikeDto>> GetFollowers(string ideaId);
        Task<IdeaDto> GetIdea(string ideaId);        
        Task<IEnumerable<IdeaCommentDto>> GetIdeaComments(string ideaId, int pageSize, int page);
        Task<IEnumerable<IdeaDto>> GetIdeas(int pageSize, int page, string keyword);
    }
}