using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Repositories;
using ProIdeas.Serializers.Contracts;

namespace ProIdeas.Infra.EventSourcing
{
    public class DefaultEventStore : IEventStore
    {
        private readonly IRepository _eventStoreRepository;
        private readonly IJsonSerializer _jsonSerializer;

        public DefaultEventStore(IRepository eventStoreRepository, IJsonSerializer jsonSerializer)
        {
            _eventStoreRepository = eventStoreRepository;
            _jsonSerializer = jsonSerializer;
            
        }

        public void Save<T>(T theEvent) where T : Event
        {
            var serializedData = _jsonSerializer.Serialize(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                string.Empty);

            _eventStoreRepository.Add(storedEvent);
        }
    }
}
