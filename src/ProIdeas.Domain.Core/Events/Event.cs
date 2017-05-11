using System;

namespace ProIdeas.Domain.Core.Events
{
    public abstract class Event : Message
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
            MessageType = "DomainEvent";
        }
    }
}