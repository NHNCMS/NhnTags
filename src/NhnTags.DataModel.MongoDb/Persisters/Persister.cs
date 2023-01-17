using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using NhnTags.DataModel.Abstracts;
using System.Linq.Expressions;

namespace NhnTags.DataModel.MongoDb.Persisters;

public class Persister<T> : IPersister<T> where T : ModelBase
{
    private readonly ILogger _logger;
    private readonly IMongoDatabase _mongoDatabase;

    public Persister(IMongoDatabase mongoDatabase, ILoggerFactory loggerFactory)
    {
        _mongoDatabase = mongoDatabase;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task<T> GetById(string id)
    {
        try
        {
            var collection = _mongoDatabase.GetCollection<T>(MapToMongoDbCollectionName()).AsQueryable();

            var results = await Task.Run(() => collection.Where(t => t.Id.Equals(id)));
            return await results.AnyAsync()
                ? results.First()
                : ConstructEntity();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task Insert(T dtoToInsert)
    {
        try
        {
            var collection = _mongoDatabase.GetCollection<T>(MapToMongoDbCollectionName());
            await collection.InsertOneAsync(dtoToInsert);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task Replace(T dtoToUpdate)
    {
        try
        {
            var collection = _mongoDatabase.GetCollection<T>(MapToMongoDbCollectionName());
            await collection.ReplaceOneAsync(x => x.Id == dtoToUpdate.Id, dtoToUpdate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task UpdateOne(string id, Dictionary<string, object> propertiesToUpdate)
    {
        try
        {
            if (!propertiesToUpdate.Any())
                return;

            var collection = _mongoDatabase.GetCollection<T>(MapToMongoDbCollectionName());

            var updateDefinition = propertiesToUpdate
                .Select(dataField => Builders<T>.Update.Set(dataField.Key, dataField.Value)).ToList();
            var combinedUpdate = Builders<T>.Update.Combine(updateDefinition);

            var updateResult = await collection.UpdateOneAsync(
                Builders<T>.Filter.Eq("_id", id),
                combinedUpdate);

            if (!updateResult.IsAcknowledged)
                throw new Exception($"Failed to Update {typeof(T).Name}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<string> Delete(string id)
    {
        try
        {
            var collection = _mongoDatabase.GetCollection<T>(MapToMongoDbCollectionName());
            var filter = Builders<T>.Filter.Eq("_id", id);
            await collection.DeleteOneAsync(filter);

            return id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task DeleteMany(Expression<Func<T, bool>> filter)
    {
        try
        {
            var collection = _mongoDatabase.GetCollection<T>(MapToMongoDbCollectionName());
            await collection.DeleteManyAsync(filter);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>>? filter = null)
    {
        try
        {
            var collection = _mongoDatabase.GetCollection<T>(MapToMongoDbCollectionName()).AsQueryable();

            return await Task.Run(() => filter != null
                ? collection.Where(filter)
                : collection);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public T ConstructEntity()
    {
        return (T)Activator.CreateInstance(typeof(T), true);
    }


    private static string MapToMongoDbCollectionName()
    {
        return typeof(T).Name;
    }
}