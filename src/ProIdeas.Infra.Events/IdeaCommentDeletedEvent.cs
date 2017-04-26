using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO;

namespace ProIdeas.Infra.Events
{
    public class IdeaCommentDeletedEvent : Event
    {
        public IdeaCommentDto Comment { get; private set; }

        public IdeaCommentDeletedEvent(IdeaCommentDto ideaComment)
        {
            Comment = ideaComment;
        }
    }
}
