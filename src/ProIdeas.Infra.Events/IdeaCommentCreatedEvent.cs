using ProIdeas.Domain.Core.Events;
using ProIdeas.DTO;

namespace ProIdeas.Infra.Events
{
    public class IdeaCommentCreatedEvent : Event
    {
        public IdeaCommentDto Comment { get; private set; }

        public IdeaCommentCreatedEvent(IdeaCommentDto ideaComment)
        {
            Comment = ideaComment;
        }
    }
}
