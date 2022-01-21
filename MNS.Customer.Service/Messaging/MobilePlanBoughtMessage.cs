using MNS.Core.Entities;
using MNS.Integration.MessageBus;

namespace MNS.Services.Customer.Api.Messaging
{
    /// <summary>
    /// Mobile Plan Buying message
    /// </summary>
    public class MobilePlanBoughtMessage : IntegrationBaseMessage
    {
        //Mobile Plan Info
        public int Plan_ID { get; set; }
        public int CustomerAge { get; set; }
        public CustomerType CustomerType { get; set; }
        public int Amount { get; set; }
        public int ValidityPeriod { get; set; }
    }
}
