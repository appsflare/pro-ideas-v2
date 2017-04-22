using ProIdeas.Domain.Core.Commands;
using ProIdeas.DTO;

namespace ProIdeas.Infra.Commands.Idea
{
    public class CreateIdeaCommand : Command
    {
        public IdeaDto Idea { get; private set; }

        public CreateIdeaCommand(IdeaDto idea)
        {
            Idea = idea;
        }

        public override bool IsValid()
        {
            return Idea != null
                && !string.IsNullOrEmpty(Idea.Title?.Trim())
                && !string.IsNullOrEmpty(Idea.Description.Trim());
        }
    }
}
