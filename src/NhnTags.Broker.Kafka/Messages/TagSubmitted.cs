using OpenSleigh.Core.Messaging;

namespace NhnTags.Broker.Kafka.Messages;

public record TagSubmitted(Guid Id, Guid CorrelationId, string TagId, string TagDescription) : IMessage;