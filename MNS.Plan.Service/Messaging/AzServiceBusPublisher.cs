using MNS.Services.MobilePlan.Core.Entities;
using System;

namespace MNS.Services.Utilization.Messaging
{
    public class AzServiceBusPublisher : IAzServiceBusPublisher
    {
        public void Publish(Plan mobilePlan)
        {
            throw new NotImplementedException();
        }
    }

    interface IAzServiceBusPublisher
    {
        public void Publish(Plan mobilePlan);
    }
}
