using NhnTags.Broker.Kafka;

namespace NhnTags.Modules;

public class KafkaModule:IModule
{
    public bool IsEnabled => true;
    public int Order => 9;
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddKafka(builder.Configuration);
        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
   
}