using OpenSleigh.Core.Messaging;

namespace NhnTags.Broker.Kafka;

public interface IServiceBus
{
    Task Publish(IMessage message, CancellationToken token);
}