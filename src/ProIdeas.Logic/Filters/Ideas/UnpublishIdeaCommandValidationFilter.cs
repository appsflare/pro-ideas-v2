using ProIdeas.Authentication.Contracts;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Repositories;
using ProIdeas.Infra.Commands.Ideas;
using System;
using System.Threading.Tasks;

namespace ProIdeas.Logic.Filters.Ideas
{
    public class UnpublishIdeaCommandValidationFilter : BaseMessageValidationFilter<UnpublishIdeaCommand>
    {
        private readonly IUserIdentityProvider _userIdentityProvider;
        private readonly IRepository _repository;
        public UnpublishIdeaCommandValidationFilter(IUserIdentityProvider userIdentityProvider, IRepository repository)
        {
            _userIdentityProvider = userIdentityProvider;
            _repository = repository;
        }

        protected override Task Validate(FilterContext<UnpublishIdeaCommand> context)
        {
            if (!context.Message.IsValid())
            {
                throw new ArgumentException(nameof(context.Message));
            }

            var idea = _repository.GetOne<Idea>(context.Message.IdeaId);

            if (idea.OwnerId != _userIdentityProvider.GetUserId())
            {
                throw new UnauthorizedAccessException();
            }

            return Task.CompletedTask;
        }
    }
}
