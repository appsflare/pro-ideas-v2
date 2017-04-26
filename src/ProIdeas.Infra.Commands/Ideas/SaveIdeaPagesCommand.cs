using ProIdeas.Domain.Core.Commands;
using ProIdeas.DTO;
using System.Collections.Generic;

namespace ProIdeas.Infra.Commands.Ideas
{
    public class SaveIdeaPagesCommand : Command
    {
        public string IdeaId { get; private set; }

        public IEnumerable<PageDto> Pages { get; private set; }

        public SaveIdeaPagesCommand(string ideaId, 
            IEnumerable<PageDto> pages)
        {
            IdeaId = ideaId;
            Pages = pages;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(IdeaId) && Pages != null;
        }
    }
}
