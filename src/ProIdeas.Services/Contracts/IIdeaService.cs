using ProIdeas.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Services.Contracts
{
    public interface IIdeaService
    {
        Task<IdeaDto> GetIdeaAsync(string ideaId);

        Task<IdeaDto> CreateAsync(IdeaDto idea);

        Task UpdateAsync(IdeaDto idea);

        Task SavePagesAsync(string ideaId, IEnumerable<PageDto> pages);

        Task PublishAsync(string ideaId);

        Task UnpublishAsync(string ideaId);

        Task<IEnumerable<IdeaDto>> GetUserIdeasAsync(string userId, int pageSize, int page, string keyword);

        Task<IEnumerable<IdeaDto>> GetIdeasAsync(int pageSize, int page);

        Task<IEnumerable<IdeaDto>> SearchIdeasAsync(string keyword, int pageSize, int page);

    }
}
