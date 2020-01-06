using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi
{
    public interface IQueueClientFactory
    {
        IQueueClient GetQueueClient();
    }

    public class QueueClientFactory : IQueueClientFactory
    {
        private readonly string connectionString;

        public QueueClientFactory(string serviceBusNamespace = null)
        {
            if (string.IsNullOrEmpty(serviceBusNamespace))
                serviceBusNamespace = "[connection string goes here!]";

            connectionString = serviceBusNamespace;
        }

        public IQueueClient GetQueueClient()
        {
            var queueClient = new QueueClient(new ServiceBusConnectionStringBuilder(connectionString));

            return queueClient;
        }
    }
}
