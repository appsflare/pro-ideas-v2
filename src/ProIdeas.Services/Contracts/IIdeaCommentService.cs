using ProIdeas.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Services.Contracts
{
    public interface IIdeaCommentService
    {
        Task<IdeaCommentDto> GetCommentAsync(string commentId);

        Task<IEnumerable<IdeaCommentDto>> GetCommentsAsync(string ideaId);

        Task<IdeaCommentDto> CreateAsync(IdeaCommentDto comment);

        void Update(IdeaCommentDto comment);      

    }
}
