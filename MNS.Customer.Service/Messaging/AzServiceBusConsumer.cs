using MNS.Services.Customer.Core.Entities;
using System;

namespace MNS.Services.Customer.Messaging
{
    public class AzServiceBusConsumer : IAzServiceBusConsumer
    {
        public void Publish(User customer)
        {
            throw new NotImplementedException();
        }
    }

    interface IAzServiceBusConsumer
    {
        public void Publish(User customer);
    }
}
