using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using MNS.Integration.MessageBus;
using MNS.Services.Utilization.Api.Messaging;
using MNS.Services.Utilization.Core.Entities;
using MNS.Services.Utilization.Infrastructure.Repos;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MNS.Utilization.Service.Api.Messaging
{
    /// <summary>
    /// Cosume the published messages
    /// </summary>
    public class AzServiceBusConsumer : IAzServiceBusConsumer
    {
        private readonly string subscriptionName = "customerbillingcycle";
        private readonly IReceiverClient billingCycleMessageReceiverClient;

        private readonly IConfiguration configuration;
        private readonly IMessageBus messageBus;
        private readonly IUtilizationRepository utilizationRepository;

        private readonly string customerRegisteredMessageTopic;
        private readonly string billingCycleRequestMessageTopic;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="messageBus"></param>
        /// <param name="utilizationRepository"></param>
        public AzServiceBusConsumer(IConfiguration configuration, IMessageBus messageBus, IUtilizationRepository utilizationRepository)
        {
            this.configuration = configuration;
            this.utilizationRepository = utilizationRepository;
            this.messageBus = messageBus;

            var serviceBusConnectionString = configuration.GetValue<string>("ServiceBusConnectionString");
            this.customerRegisteredMessageTopic = configuration.GetValue<string>("CustomerRegisteredMessageTopic");
            this.billingCycleRequestMessageTopic = configuration.GetValue<string>("BillingCycleRequestMessageTopic");

            this.billingCycleMessageReceiverClient = new SubscriptionClient(serviceBusConnectionString, customerRegisteredMessageTopic, subscriptionName);
        }

        /// <summary>
        /// Start receiving the publsihed messages
        /// </summary>
        public void Start()
        {
            var messageHandlerOptions = new MessageHandlerOptions(OnServiceBusException) { MaxConcurrentCalls = 4 };

            billingCycleMessageReceiverClient.RegisterMessageHandler(OnBillingCycleGenerateMessageReceived, messageHandlerOptions);
        }

        public void Stop()
        {

        }

        private async Task OnBillingCycleGenerateMessageReceived(Message message, CancellationToken arg2)
        {
            var body = Encoding.UTF8.GetString(message.Body);//json from service bus

            //save order with status not paid
            BillingCycleGeneratedMessage billingCycleGeneratedMessage = JsonConvert.DeserializeObject<BillingCycleGeneratedMessage>(body);

            Consumption consumption = new Consumption()
            {
                Billing_Id = Guid.NewGuid(),
                Customer_Id = billingCycleGeneratedMessage.Customer_Id,
                Plan_Id = billingCycleGeneratedMessage.Plan_Id,
                BillingCycle = billingCycleGeneratedMessage.BillingCycle
            };

            await utilizationRepository.AddBilling(consumption);
            try
            {
                await messageBus.PublishMessage(billingCycleGeneratedMessage, billingCycleRequestMessageTopic);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private Task OnServiceBusException(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine(exceptionReceivedEventArgs);

            return Task.CompletedTask;
        }
    }
}
