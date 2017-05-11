using System;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Domain.Core.Commands;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Core.Notifications;
using System.Threading.Tasks;

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

        public Task SendCommand<T>(T theCommand) where T : Command
        {
            return Publish(theCommand);
        }

        public Task RaiseEvent<T>(T theEvent) where T : Event
        {
            if (!theEvent.MessageType.Equals("DomainNotification"))
            { _eventStore?.Save(theEvent); }

            return Publish(theEvent);
        }

        async private Task Publish<T>(T message) where T : Message
        {
            if (_container == null) return;

            var filter = _container.GetService(typeof(IMessageFilter<T>)) as IMessageFilter<T>;
            var filterContext = new FilterContext<T>(message);
            await filter?.Execute(filterContext);

            var obj = _container.GetService(message.MessageType.Equals("DomainNotification")
                ? typeof(IDomainNotificationHandler<T>)
                : typeof(IHandler<T>));

            await ((IHandler<T>)obj)?.Handle(filterContext.Message);
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