using ProIdeas.Authentication.Contracts;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Repositories;
using ProIdeas.Infra.Commands.Collaboration;
using System;

namespace ProIdeas.Logic.Filters.Collaboration
{
    public class LikeIdeaCommandValidationFilter : BaseMessageValidationFilter<LikeIdeaCommand>
    {
        private readonly IUserIdentityProvider _userIdentityProvider;
        private readonly IRepository _repository;
        public LikeIdeaCommandValidationFilter(IUserIdentityProvider userIdentityProvider, IRepository repository)
        {
            _userIdentityProvider = userIdentityProvider;
            _repository = repository;
        }

        protected override void Validate(FilterContext<LikeIdeaCommand> context)
        {
            if (!context.Message.IsValid())
            { throw new ArgumentException(nameof(context.Message)); }

            var idea = _repository.GetOne<Idea>(context.Message.IdeaId);
            if (idea == null)
            {
                throw new InvalidOperationException("invalid idea");
            }


            //TODO: perform user validation as well


        }
    }
}
