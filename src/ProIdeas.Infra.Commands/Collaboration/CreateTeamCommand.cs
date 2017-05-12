using ProIdeas.Domain.Core.Commands;
using ProIdeas.DTO;

namespace ProIdeas.Infra.Commands.Collaboration
{
    public class CreateTeamCommand : Command
    {
        public TeamDto Team { get; private set; }
        public CreateTeamCommand(TeamDto team)
        {
            Team = team;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(Team.IdeaId?.Trim()) && !string.IsNullOrEmpty(Team.Name?.Trim());
        }
    }
}
