using System;
using System.Threading.Tasks;

namespace ProIdeas.Domain.Core.Events
{
    public abstract class BaseMessageValidationFilter<T> : IMessageFilter<T> where T : Message
    {
        public Task Execute(FilterContext<T> context)
        {
            if (context == null)
            { throw new ArgumentNullException(nameof(context)); }

            if (context.Message == null)
            { throw new ArgumentNullException(nameof(context.Message)); }


            return Validate(context);
        }

        protected abstract Task Validate(FilterContext<T> context);
    }
}
