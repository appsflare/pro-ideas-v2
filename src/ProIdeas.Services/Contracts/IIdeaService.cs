using ProIdeas.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Services.Contracts
{
    public interface IIdeaService
    {
        Task<IdeaDto> GetIdeaAsync(string ideaId);

        Task<IdeaDto> CreateAsync(IdeaDto idea);

        Task<IEnumerable<IdeaDto>> GetIdeasAsync(int pageSize, int page);

    }
}
