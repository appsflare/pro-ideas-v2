using ProIdeas.Authentication.Contracts;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Repositories;
using ProIdeas.Infra.Commands.Comments;
using System;

namespace ProIdeas.Logic.Filters.Comments
{
    public class UpdateIdeaCommentCommandValidationFilter : BaseMessageValidationFilter<UpdateIdeaCommentCommand>
    {
        private readonly IUserIdentityProvider _userIdentityProvider;
        private readonly IRepository _repository;
        public UpdateIdeaCommentCommandValidationFilter(IUserIdentityProvider userIdentityProvider, IRepository repository)
        {
            _userIdentityProvider = userIdentityProvider;
            _repository = repository;
        }

        protected override void Validate(FilterContext<UpdateIdeaCommentCommand> context)
        {
            if (!context.Message.IsValid())
            {
                throw new ArgumentException(nameof(context.Message));
            }

            var idea = _repository.GetOne<Idea>(context.Message.Comment.IdeaId);

            if(idea == null)
            {
                throw new InvalidOperationException("invalid idea");
            }

            var comment = _repository.GetOne<IdeaComment>(context.Message.Comment.Id);

            if (comment.UserId != _userIdentityProvider.GetUserId())
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
