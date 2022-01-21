using MNS.Services.Utilization.Core.Entities;
using System;

namespace MNS.Utilization.Service.Api.Messaging
{
    public class AzServiceBusPublisher : IAzServcieBusPublisher
    {
        public void Publish(Consumption utilization)
        {
            throw new NotImplementedException();
        }
    }

    interface IAzServcieBusPublisher
    {
        public void Publish(Consumption utilization);
    }
}
