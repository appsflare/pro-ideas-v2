using ProIdeas.Authentication.Contracts;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Infra.Commands.Idea;
using System;

namespace ProIdeas.Logic.Filters
{
    public class UpdateIdeaCommandValidationFilter : BaseMessageValidationFilter<UpdateIdeaCommand>
    {
        private readonly IUserIdentityProvider _userIdentityProvider;
        public UpdateIdeaCommandValidationFilter(IUserIdentityProvider userIdentityProvider)
        {
            _userIdentityProvider = userIdentityProvider;
        }

        protected override void Validate(FilterContext<UpdateIdeaCommand> context)
        {
            if (!context.Message.IsValid())
            {
                throw new ArgumentException(nameof(context.Message));
            }

            if (context.Message.Idea.OwnerId != _userIdentityProvider.GetUserId())
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
