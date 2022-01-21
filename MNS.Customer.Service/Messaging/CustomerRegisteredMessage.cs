using MNS.Integration.MessageBus;

namespace MNS.Services.Customer.Api.Messaging
{
    /// <summary>
    /// Customer Registered Message
    /// </summary>
    public class CustomerRegisteredMessage : IntegrationBaseMessage
    {
        //User Info
        public int Customer_Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public int Plan_Id { get; set; }
        public bool IsVerified { get; set; }
    }
}
