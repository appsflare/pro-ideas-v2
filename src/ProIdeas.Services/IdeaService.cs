using ProIdeas.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using ProIdeas.DTO;
using ProIdeas.Logic.Contracts;
using System.Threading.Tasks;

namespace ProIdeas.Services
{
    public class IdeaService : IIdeaService
    {
        private readonly IIdeaLogic _ideaLogic;
        public IdeaService(IIdeaLogic ideaLogic)
        {
            _ideaLogic = ideaLogic;

        }

        public Task<IdeaDto> CreateAsync(IdeaDto idea)
        {
            return _ideaLogic.CreateIdea(idea);
        }

        public Task<IdeaDto> GetIdeaAsync(string ideaId)
        {
            return _ideaLogic.GetIdea(ideaId);
        }

        public Task<IEnumerable<IdeaDto>> GetIdeasAsync(int pageSize, int page)
        {
            return _ideaLogic.GetIdeas(pageSize, page, string.Empty);
        }
    }
}
