using ProIdeas.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Services.Contracts
{
    public interface IIdeaService
    {
        Task<IdeaDto> GetIdeaAsync(string ideaId);

        Task<IdeaDto> CreateAsync(IdeaDto idea);

        Task Update(IdeaDto idea);

        Task SavePages(string ideaId, IEnumerable<PageDto> pages);

        Task Publish(string ideaId);

        Task Unpublish(string ideaId);

        Task<IEnumerable<IdeaDto>> GetUserIdeasAsync(string userId, int pageSize, int page, string keyword);

        Task<IEnumerable<IdeaDto>> GetIdeasAsync(int pageSize, int page);

        Task<IEnumerable<IdeaDto>> SearchIdeasAsync(string keyword, int pageSize, int page);

    }
}
