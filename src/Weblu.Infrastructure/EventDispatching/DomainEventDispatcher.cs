using Microsoft.Extensions.DependencyInjection;
using Weblu.Application.Common.Interfaces;
using Weblu.Domain.Interfaces;

namespace Weblu.Infrastructure.EventDispatching
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task DispatchAsync(IEnumerable<object> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                var handlerType = typeof(IDomainEventHandler<>)
                    .MakeGenericType(domainEvent.GetType());

                var handlers = _serviceProvider.GetServices(handlerType);
                if (handlers.Any())
                {
                    foreach (dynamic handler in handlers)
                    {
                        await handler.Handle((dynamic)domainEvent);
                    }
                }
            }
        }
    }
}