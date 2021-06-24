using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.IntegrationEvents.Events
{
    public class OrderPaymentFailedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }

        public OrderPaymentFailedIntegrationEvent()
        {
        }

        public OrderPaymentFailedIntegrationEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
