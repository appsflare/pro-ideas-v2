using System;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Domain.Core.Commands;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Core.Notifications;

namespace ProIdeas.Infra.Bus
{
    public sealed class InMemoryBus : IBus
    {
        private readonly IServiceProvider _container;
        private readonly IEventStore _eventStore;

        public InMemoryBus(IServiceProvider serviceProvider, IEventStore eventStore)
        {
            _container = serviceProvider;
            _eventStore = eventStore;
        }

        public void SendCommand<T>(T theCommand) where T : Command
        {
            Publish(theCommand);
        }

        public void RaiseEvent<T>(T theEvent) where T : Event
        {
            if (!theEvent.MessageType.Equals("DomainNotification"))
            { _eventStore?.Save(theEvent); }

            Publish(theEvent);
        }

        private void Publish<T>(T message) where T : Message
        {
            if (_container == null) return;

            var obj = _container.GetService(message.MessageType.Equals("DomainNotification")
                ? typeof(IDomainNotificationHandler<T>)
                : typeof(IHandler<T>));

            ((IHandler<T>)obj).Handle(message);
        }

        private object GetService(Type serviceType)
        {
            return _container.GetService(serviceType);
        }

        private T GetService<T>()
        {
            return (T)_container.GetService(typeof(T));
        }
    }
}