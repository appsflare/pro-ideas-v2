using ProIdeas.Domain.Core.Events;

namespace ProIdeas.Infra.EventSourcing
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}