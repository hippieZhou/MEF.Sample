using BlankApp.Doamin.Bus;
using Prism.Events;
using System;

namespace BlankApp.Infrastructure.Bus
{
    public class InMemoryBus : IEventBus
    {
        private readonly IEventAggregator _eventAggregator;

        public InMemoryBus(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        }

        public void Sned<PubSubEvent>(object payload)
        {
        }
    }
}
