using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NhnTags.DataModel.Abstracts;
using NhnTags.DataModel.Models;
using NhnTags.DataModel.MongoDb.Persisters;
using NhnTags.Shared.Configuration;

namespace NhnTags.DataModel.MongoDb;

public static class MongoDbHelper
{
    public static IServiceCollection AddMongoDbPersister(this IServiceCollection services)
    {
        services.AddSingleton(serviceProvider =>
        {
            var mongoDbParameter = serviceProvider
                .GetRequiredService<IOptions<AppSettings>>()
                .Value.MongoDbParameters;

            var client = new MongoClient(mongoDbParameter.ConnectionString);
            var database = client.GetDatabase(mongoDbParameter.DatabaseName);
            return database;
        });

        services.AddScoped<IPersister<TagModel>, Persister<TagModel>>();

        return services;
    }
}