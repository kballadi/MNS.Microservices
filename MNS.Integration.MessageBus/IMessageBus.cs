using System.Threading.Tasks;

namespace MNS.Integration.MessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(IntegrationBaseMessage message, string topicName);
    }
}
