using ProIdeas.Authentication.Contracts;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Entities.Tasks;
using ProIdeas.Domain.Repositories;
using ProIdeas.Exceptions;
using ProIdeas.Infra.Commands.Tasks;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.Logic.Filters.Tasks
{
    public class UpdateTaskBoardCommandValidationFilter : BaseMessageValidationFilter<UpdateTaskBoardCommand>
    {
        private readonly IUserIdentityProvider _userIdentityProvider;
        private readonly IRepository _repository;
        public UpdateTaskBoardCommandValidationFilter(IUserIdentityProvider userIdentityProvider, IRepository repository)
        {
            _userIdentityProvider = userIdentityProvider;
            _repository = repository;
        }

        protected override async Task Validate(FilterContext<UpdateTaskBoardCommand> context)
        {
            var board = await _repository.GetOneAsync<TaskBoard>(context.Message.TaskBoard.Id);

            if (board == null)
            {
                throw new LogicalException(ErrorCategory.NotFound, "Board not found");
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
