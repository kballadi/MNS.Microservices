using MNS.Services.Customer.Core.Entities;
using System;

namespace MNS.Services.Customer.Messaging
{
    public class AzServiceBusPublisher : IAzServiceBusPublisher
    {
        public void Publish(User customer)
        {
            throw new NotImplementedException();
        }
    }

    interface IAzServiceBusPublisher
    {
        public void Publish(User customer);
    }
}
