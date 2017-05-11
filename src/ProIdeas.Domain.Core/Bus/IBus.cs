using ProIdeas.Domain.Core.Commands;
using ProIdeas.Domain.Core.Events;
using System.Threading.Tasks;

namespace ProIdeas.Domain.Core.Bus
{
    public interface IBus
    {
        Task SendCommand<T>(T theCommand) where T : Command;
        Task RaiseEvent<T>(T theEvent) where T : Event;
    }
}