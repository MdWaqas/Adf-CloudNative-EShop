using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.IntegrationEvents.Events
{
    public class OrderStatusChangedToSubmittedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public string OrderStatus { get; set; }
        public string BuyerId { get; set; }
        public string BuyerName { get; set; }
    }
}
