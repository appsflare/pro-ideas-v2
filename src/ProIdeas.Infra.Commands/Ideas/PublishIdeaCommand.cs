using ProIdeas.Domain.Core.Commands;

namespace ProIdeas.Infra.Commands.Ideas
{
    public class PublishIdeaCommand : Command
    {
        public string IdeaId { get; private set; }

        public PublishIdeaCommand(string ideaId)
        {
            IdeaId = ideaId;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(IdeaId);
        }
    }
}
