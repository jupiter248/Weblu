using Weblu.Domain.Events.Common;

namespace Weblu.Domain.Interfaces.Events
{
    public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
    {
        Task Handle(TEvent domainEvent);
    }
}