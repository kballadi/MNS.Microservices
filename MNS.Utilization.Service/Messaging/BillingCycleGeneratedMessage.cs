using MNS.Integration.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MNS.Services.Utilization.Api.Messaging
{
    public class BillingCycleGeneratedMessage : IntegrationBaseMessage
    {
        public int Billing_Id { get; set; }
        public int Customer_Id { get; set; }
        public int Plan_Id { get; set; }
        public int BillingCycle { get; set; }
    }
}
