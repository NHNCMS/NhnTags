using OpenSleigh.Core.Messaging;

namespace NhnTags.Broker.Kafka;

public class ServiceBus : IServiceBus
{
    private readonly IMessageBus _bus;
    public ServiceBus(IMessageBus bus )
    {
        _bus = bus;
    }

    public async Task Publish(IMessage message, CancellationToken token)
    {
        await _bus.PublishAsync(message, token);
    }
}