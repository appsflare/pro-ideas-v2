using ProIdeas.Domain.Core.Commands;

namespace ProIdeas.Infra.Commands.Idea
{
    public class DeleteIdeaCommand : Command
    {
        public string   IdeaId { get; private set; }

        public DeleteIdeaCommand(string ideaId)
        {
            IdeaId = ideaId;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(IdeaId);
        }
    }
}
