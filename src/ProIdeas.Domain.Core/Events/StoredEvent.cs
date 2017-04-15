using ProIdeas.Domain.Repositories;
using System;

namespace ProIdeas.Domain.Core.Events
{
    public class StoredEvent : Event, IEntity
    {
        public StoredEvent(Event theEvent, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }

        public StoredEvent()
        {

        }
        

        public object Id { get; set; }

        public string Data { get; private set; }

        public string User { get; private set; }
    }
}