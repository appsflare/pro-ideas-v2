using ProIdeas.Domain.Core.Commands;
using ProIdeas.Domain.Core.Events;

namespace ProIdeas.Domain.Core.Bus
{
    public interface IBus
    {
        void SendCommand<T>(T theCommand) where T : Command;
        void RaiseEvent<T>(T theEvent) where T : Event;
    }
}