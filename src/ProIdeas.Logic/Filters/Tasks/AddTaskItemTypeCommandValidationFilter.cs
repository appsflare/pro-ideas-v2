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
    public class AddTaskItemTypeCommandValidationFilter : BaseMessageValidationFilter<AddTaskItemTypeCommand>
    {
        private readonly IUserIdentityProvider _userIdentityProvider;
        private readonly IRepository _repository;
        public AddTaskItemTypeCommandValidationFilter(IUserIdentityProvider userIdentityProvider, IRepository repository)
        {
            _userIdentityProvider = userIdentityProvider;
            _repository = repository;
        }

        protected override async Task Validate(FilterContext<AddTaskItemTypeCommand> context)
        {
            var taskBoard = await _repository.GetOneAsync<TaskBoard>(context.Message.BoardId);

            if (taskBoard == null)
            {
                throw new LogicalException(ErrorCategory.InvalidInput, "Invalid board");
            }

            var team = await _repository.GetOneAsync<Team>(taskBoard.TeamId);

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
