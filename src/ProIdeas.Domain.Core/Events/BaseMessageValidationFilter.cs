using System;

namespace ProIdeas.Domain.Core.Events
{
    public class BaseMessageValidationFilter<T> : IMessageFilter<T> where T : Message
    {
        public void Execute(FilterContext<T> context)
        {
            if (context == null)
            { throw new ArgumentNullException(nameof(context)); }

            if (context.Message == null)
            { throw new ArgumentNullException(nameof(context.Message)); }


            Validate(context);
        }

        protected virtual void Validate(FilterContext<T> context)
        {

        }
    }
}
