namespace ProIdeas.Domain.Core.Events
{
    public class FilterContext<T> where T : Message
    {
        public FilterContext(T message)
        {
            Message = message;
        }

        public T Message { get; private set; }

    }
}
