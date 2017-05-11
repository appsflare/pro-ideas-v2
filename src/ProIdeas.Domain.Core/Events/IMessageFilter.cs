using System.Threading.Tasks;

namespace ProIdeas.Domain.Core.Events
{
    public interface IMessageFilter<T> where T : Message
    {
        Task Execute(FilterContext<T> context);
    }
}
