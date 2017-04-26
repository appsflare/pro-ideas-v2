using ProIdeas.Domain.Core.Commands;

namespace ProIdeas.Infra.Commands.Ideas
{
    public class UnpublishIdeaCommand : Command
    {
        public string IdeaId { get; private set; }

        public UnpublishIdeaCommand(string ideaId)
        {
            IdeaId = ideaId;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(IdeaId);
        }
    }
}
