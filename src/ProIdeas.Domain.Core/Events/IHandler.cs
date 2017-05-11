using System.Threading.Tasks;

namespace ProIdeas.Domain.Core.Events
{
    public interface IHandler<in T> where T : Message
    {
        Task Handle(T message);
    }
}