using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapr.Client;
using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Logging;

namespace EventBus
{
    public class DaprEventBus : IEventBus
    {
        private const string DaprPubsubName = "pubsub";

        private readonly DaprClient _dapr;
        private readonly ILogger<DaprEventBus> _logger;

        public DaprEventBus(DaprClient dapr, ILogger<DaprEventBus> logger)
        {
            _dapr = dapr;
            _logger = logger;
        }

        public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent @event) where TIntegrationEvent : IntegrationEvent
        {
            var topicName = @event.GetType().Name;

            _logger.LogInformation("Publishing event {@Event} to {PubsubName}.{TopicName}", @event, DaprPubsubName, topicName);

            // We need to make sure that we pass the concrete type to PublishEventAsync,
            // which can be accomplished by casting the event to dynamic. This ensures
            // that all event fields are properly serialized.
            await _dapr.PublishEventAsync(DaprPubsubName, topicName, (dynamic)@event);
        }
    }
}
