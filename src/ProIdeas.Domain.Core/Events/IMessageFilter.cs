namespace ProIdeas.Domain.Core.Events
{
    public interface IMessageFilter<T> where T : Message
    {
        void Execute(FilterContext<T> context);
    }
}
