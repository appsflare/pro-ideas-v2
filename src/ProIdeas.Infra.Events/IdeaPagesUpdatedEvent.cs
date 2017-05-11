using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO;
using System.Collections.Generic;

namespace ProIdeas.Infra.Events
{
    public class IdeaPagesUpdatedEvent : Event
    {
        public string IdeaId { get; private set; }

        public IEnumerable<PageDto> Pages{ get; private set; }

        public IdeaPagesUpdatedEvent(string ideaId, IEnumerable<PageDto> pages)
        {
            IdeaId = ideaId;
            Pages = pages;
        }
    }
}
