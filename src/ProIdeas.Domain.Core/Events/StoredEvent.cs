using ProIdeas.Domain.Repositories;

namespace ProIdeas.Domain.Core.Events
{
    public class StoredEvent : Event, IEntity
    {
        public StoredEvent(Event theEvent, string data, string user)
        {            
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }

        public StoredEvent()
        {

        }
        

        public string Id { get; set; }

        public string Data { get; private set; }

        public string User { get; private set; }
    }
}