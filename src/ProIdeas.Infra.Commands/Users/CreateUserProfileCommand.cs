using ProIdeas.Domain.Core.Commands;

namespace ProIdeas.Infra.Commands.Users
{
    public class CreateUserProfileCommand : Command
    {
        public CreateUserProfileCommand(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; private set; }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(UserId?.Trim());
        }
    }
}
