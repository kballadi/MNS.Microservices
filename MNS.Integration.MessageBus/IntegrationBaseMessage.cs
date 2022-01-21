using System;

namespace MNS.Integration.MessageBus
{
    public class IntegrationBaseMessage
    {
        public Guid Id { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
