using ProIdeas.Authentication.Contracts;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Repositories;
using ProIdeas.Infra.Commands.Collaboration;
using ProIdeas.Infra.Commands.Ideas;
using System;

namespace ProIdeas.Logic.Filters.Collaboration
{
    public class DeleteIdeaCommentCommandValidationFilter : BaseMessageValidationFilter<DeleteIdeaCommentCommand>
    {
        private readonly IUserIdentityProvider _userIdentityProvider;
        private readonly IRepository _repository;
        public DeleteIdeaCommentCommandValidationFilter(IUserIdentityProvider userIdentityProvider, IRepository repository)
        {
            _userIdentityProvider = userIdentityProvider;
            _repository = repository;
        }

        protected override void Validate(FilterContext<DeleteIdeaCommentCommand> context)
        {
            if (!context.Message.IsValid())
            {
                throw new ArgumentException(nameof(context.Message));
            }

           var comment =  _repository.GetOne<IdeaComment>(context.Message.CommentId);

            if(comment == null)
            {
                throw new InvalidOperationException("invalid comment");
            }

            if (comment.OwnerId != _userIdentityProvider.GetUserId())
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
