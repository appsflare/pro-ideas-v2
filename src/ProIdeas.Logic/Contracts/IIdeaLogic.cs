using System.Collections.Generic;
using ProIdeas.DTO;

namespace ProIdeas.Logic.Contracts
{
    public interface IIdeaLogic
    {
        IEnumerable<IdeaLikeDto> GetFollowers(string ideaId);
        IdeaDto GetIdea(string ideaId);
        IEnumerable<IdeaCommentDto> GetIdeaComments(string ideaId, int pageSize, int page);
        IEnumerable<IdeaDto> GetIdeas(int pageSize, int page, string keyword);
    }
}