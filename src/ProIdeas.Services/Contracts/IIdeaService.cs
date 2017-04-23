using ProIdeas.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Services.Contracts
{
    public interface IIdeaService
    {
        Task<IdeaDto> GetIdeaAsync(string ideaId);

        Task<IdeaDto> CreateAsync(IdeaDto idea);

        void Update(IdeaDto idea);

        void SavePages(string ideaId, IEnumerable<PageDto> pages);

        Task<IEnumerable<IdeaDto>> GetIdeasAsync(int pageSize, int page);

        Task<IEnumerable<IdeaDto>> SearchIdeasAsync(string keyword, int pageSize, int page);

    }
}
