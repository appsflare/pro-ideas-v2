using ProIdeas.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProIdeas.Services.Contracts
{
    public interface IIdeaService
    {
        IdeaDto GetIdea(string ideaId);

        IEnumerable<IdeaDto> GetIdeas(int pageSize, int page);

    }
}
