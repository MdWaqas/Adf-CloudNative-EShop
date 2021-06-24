using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Events;

namespace EventBus.Abstractions
{
    public interface IEventBus
    {
        Task PublishAsync<TIntegrationEvent>(TIntegrationEvent @event) where TIntegrationEvent : IntegrationEvent;
    }
}
