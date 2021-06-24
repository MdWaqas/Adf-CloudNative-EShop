using System;
using EventBus.Events;

namespace Payment.API.IntegrationEvents.Events
{
    public class OrderStatusChangedToValidatedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }

        public decimal Total { get; set; }

        public OrderStatusChangedToValidatedIntegrationEvent()
        {
        }
    }
}