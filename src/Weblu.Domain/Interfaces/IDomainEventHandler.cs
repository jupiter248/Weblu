using Weblu.Domain.Events.Common;

namespace Weblu.Domain.Interfaces
{
    public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
    {
        Task Handle(TEvent domainEvent);
    }
}