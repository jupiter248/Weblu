using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Events.Common
{
    public interface IDomainEvent
    {
        DateTimeOffset OccurredOn { get; }
    }
}