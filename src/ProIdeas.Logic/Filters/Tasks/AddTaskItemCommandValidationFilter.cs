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
    public class AddTaskItemCommandValidationFilter : BaseMessageValidationFilter<AddTaskItemCommand>
    {
        private readonly IUserIdentityProvider _userIdentityProvider;
        private readonly IRepository _repository;
        public AddTaskItemCommandValidationFilter(IUserIdentityProvider userIdentityProvider, IRepository repository)
        {
            _userIdentityProvider = userIdentityProvider;
            _repository = repository;
        }

        protected override async Task Validate(FilterContext<AddTaskItemCommand> context)
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

            var state = await _repository.GetOneAsync<TaskItemState>(context.Message.Item.StateId);

            if (state == null)
            {
                throw new LogicalException(ErrorCategory.InvalidInput, "Invalid task state");
            }

            var type = await _repository.GetOneAsync<TaskItemState>(context.Message.Item.TaskTypeId);

            if (type == null)
            {
                throw new LogicalException(ErrorCategory.InvalidInput, "Invalid task type");
            }
        }
    }
}
