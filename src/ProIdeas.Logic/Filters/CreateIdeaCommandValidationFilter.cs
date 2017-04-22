using ProIdeas.Domain.Core.Events;
using ProIdeas.Infra.Commands.Idea;
using System;

namespace ProIdeas.Logic.Filters
{
    public class CreateIdeaCommandValidationFilter : BaseMessageValidationFilter<CreateIdeaCommand>
    {
        protected override void Validate(FilterContext<CreateIdeaCommand> context)
        {
            if (!context.Message.IsValid())
            {
                throw new ArgumentException(nameof(context.Message));
            }
        }
    }
}
