using ProIdeas.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using ProIdeas.DTO;
using ProIdeas.Logic.Contracts;

namespace ProIdeas.Services
{
    public class IdeaService : IIdeaService
    {
        private readonly IIdeaLogic _ideaLogic;
        public IdeaService(IIdeaLogic ideaLogic)
        {
            _ideaLogic = ideaLogic;

        }

        public IdeaDto GetIdea(string ideaId)
        {
            return _ideaLogic.GetIdea(ideaId);
        }

        public IEnumerable<IdeaDto> GetIdeas(int pageSize, int page)
        {
            return _ideaLogic.GetIdeas(pageSize, page, string.Empty);
        }
    }
}
