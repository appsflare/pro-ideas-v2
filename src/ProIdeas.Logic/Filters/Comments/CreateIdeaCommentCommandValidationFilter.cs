using ProIdeas.Domain.Core.Events;
using ProIdeas.Infra.Commands.Comments;
using System;

namespace ProIdeas.Logic.Filters.Ideas
{
    public class CreateIdeaCommentCommandValidationFilter : BaseMessageValidationFilter<CreateIdeaCommentCommand>
    {
        protected override void Validate(FilterContext<CreateIdeaCommentCommand> context)
        {
            if (!context.Message.IsValid())
            {
                throw new ArgumentException(nameof(context.Message));
            }
        }
    }
}
