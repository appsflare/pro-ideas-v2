using System;
using System.Linq;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Domain.Core.Commands;
using ProIdeas.Domain.Core.Events;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProIdeas.Infra.Bus
{
    public sealed class InMemoryBus : IBus
    {
        private readonly IServiceProvider _container;
        private readonly IEventStore _eventStore;
        private static readonly string[] _eventTypes = { "DomainEvent", "DomainNotification" };

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

        private async Task Publish<T>(T message) where T : Message
        {
            if (_container == null) return;

            var filter = _container.GetService(typeof(IMessageFilter<T>)) as IMessageFilter<T>;
            if (filter != null)
            {
                var filterContext = new FilterContext<T>(message);
                await filter.Execute(filterContext);
            }

            if (_eventTypes.Contains(message.MessageType))
            {
                var handlers = _container.GetService(typeof(IEnumerable<IHandler<T>>)) as IEnumerable<IHandler<T>>;
                if (handlers == null)
                { return; }

                foreach (var handler in handlers)
                {
                    await handler.Handle(message);
                }

                return;
            }

            var commandHandler = _container.GetService(typeof(IHandler<T>));

            await ((IHandler<T>)commandHandler)?.Handle(message);

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