using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO;

namespace ProIdeas.Infra.Events
{
    public class IdeaCommentUpdatedEvent : Event
    {
        public IdeaCommentDto Comment { get; private set; }

        public IdeaCommentUpdatedEvent(IdeaCommentDto ideaComment)
        {
            Comment = ideaComment;
        }
    }
}
