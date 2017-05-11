using System.Collections.Generic;
using ProIdeas.Domain.Core.Events;

namespace ProIdeas.Domain.Core.Notifications
{
    public interface IDomainNotificationHandler<T> : IHandler<T> where T : Message
    {
        bool HasNotifications();
        IEnumerable<T> GetNotifications();
    }
}