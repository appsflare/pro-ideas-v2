using ProIdeas.Authentication.Contracts;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Repositories;
using ProIdeas.Infra.Commands.Ideas;
using System;

namespace ProIdeas.Logic.Filters.Ideas
{
    public class PublishIdeaCommandValidationFilter : BaseMessageValidationFilter<PublishIdeaCommand>
    {
        private readonly IUserIdentityProvider _userIdentityProvider;
        private readonly IRepository _repository;
        public PublishIdeaCommandValidationFilter(IUserIdentityProvider userIdentityProvider, IRepository repository)
        {
            _userIdentityProvider = userIdentityProvider;
            _repository = repository;
        }

        protected override void Validate(FilterContext<PublishIdeaCommand> context)
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
        }
    }
}
