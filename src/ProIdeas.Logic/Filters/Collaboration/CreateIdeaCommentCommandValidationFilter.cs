using ProIdeas.Domain.Core.Events;
using ProIdeas.Infra.Commands.Collaboration;
using System;
using System.Threading.Tasks;

namespace ProIdeas.Logic.Filters.Collaboration

{
    public class CreateIdeaCommentCommandValidationFilter : BaseMessageValidationFilter<CreateIdeaCommentCommand>
    {
        protected override Task Validate(FilterContext<CreateIdeaCommentCommand> context)
        {
            if (!context.Message.IsValid())
            {
                throw new ArgumentException(nameof(context.Message));
            }

            return Task.CompletedTask;
        }
    }
}
