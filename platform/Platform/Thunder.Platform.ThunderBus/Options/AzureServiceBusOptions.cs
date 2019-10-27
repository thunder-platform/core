using System;

namespace Thunder.Platform.ThunderBus.Options
{
    public class AzureServiceBusOptions
    {
        public string BusUri { get; set; }

        public string BusKeyName { get; set; }

        public string BusSharedAccessKey { get; set; }

        public int? PrefetchCount { get; set; }

        public int? TokenTimeToLive { get; set; }

        public Microsoft.Azure.ServiceBus.Primitives.TokenScope TokenScope { get; set; }

        public Uri QueueUri(string queueName)
        {
            return new Uri($"{BusUri}/queueName");
        }
    }
}
