using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NhnTags.Broker.Kafka.Messages;
using NhnTags.Shared.Configuration;
using OpenSleigh.Core.DependencyInjection;
using OpenSleigh.Persistence.Mongo;
using OpenSleigh.Transport.Kafka;

namespace NhnTags.Broker.Kafka;

public static class KafkaHelper
{
    public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
    {

        var parameters = services.BuildServiceProvider()
            .GetRequiredService<IOptions<AppSettings>>().Value;

        var mongoDbParameter = parameters.MongoDbParameters;
        var kafkaConfig = new KafkaConfiguration(parameters.KafkaParameters.ConnectionString);

        var mongoCfg = new MongoConfiguration(mongoDbParameter.ConnectionString,
            mongoDbParameter.DatabaseName);
        services.AddOpenSleigh(cfg =>
        {
            // code omitted
            cfg.UseKafkaTransport(kafkaConfig,
                builder =>
                {
                    builder.UseMessageNamingPolicy<TagSubmitted>(() => new QueueReferences("tags", "tags.dead"));
                });

            cfg.UseMongoPersistence(mongoCfg);
        });
        services.AddScoped<IServiceBus, ServiceBus>();
        return services;
    }
}