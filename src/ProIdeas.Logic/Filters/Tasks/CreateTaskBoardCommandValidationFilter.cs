using ProIdeas.Authentication.Contracts;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Repositories;
using ProIdeas.Exceptions;
using ProIdeas.Infra.Commands.Tasks;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.Logic.Filters.Tasks
{
    public class CreateTaskBoardCommandValidationFilter : BaseMessageValidationFilter<CreateTaskBoardCommand>
    {
        private readonly IUserIdentityProvider _userIdentityProvider;
        private readonly IRepository _repository;
        public CreateTaskBoardCommandValidationFilter(IUserIdentityProvider userIdentityProvider, IRepository repository)
        {
            _userIdentityProvider = userIdentityProvider;
            _repository = repository;
        }

        protected override async Task Validate(FilterContext<CreateTaskBoardCommand> context)
        {
            var idea = await _repository.GetOneAsync<Idea>(context.Message.IdeaId);

            if (idea == null)
            {
                throw new LogicalException(ErrorCategory.InvalidInput, "Invalid idea");
            }

            var team = await _repository.GetOneAsync<Team>(context.Message.TaskBoard.TeamId);

            if (team == null)
            {
                throw new LogicalException(ErrorCategory.InvalidInput, "Invalid team");
            }

            if (!team.Members.Any(i => i.MemberUserId == _userIdentityProvider.GetUserId()))
            {
                throw new LogicalException(ErrorCategory.UnAuthorized);
            }

        }
    }
}
