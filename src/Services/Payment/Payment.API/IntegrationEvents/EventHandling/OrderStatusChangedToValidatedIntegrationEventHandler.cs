using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Payment.API.IntegrationEvents.Events;
using Serilog.Context;

namespace Payment.API.IntegrationEvents.EventHandling
{
    public class OrderStatusChangedToValidatedIntegrationEventHandler : IIntegrationEventHandler<OrderStatusChangedToValidatedIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly PaymentSettings _settings;
        private readonly ILogger<OrderStatusChangedToValidatedIntegrationEventHandler> _logger;

        public OrderStatusChangedToValidatedIntegrationEventHandler(IEventBus eventBus, IOptionsSnapshot<PaymentSettings> settings, ILogger<OrderStatusChangedToValidatedIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _settings = settings.Value;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));

            _logger.LogTrace("PaymentSettings: {@PaymentSettings}", _settings);
        }

        public Task Handle(OrderStatusChangedToValidatedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                throw new NotImplementedException();
            }
        }
    }
}
