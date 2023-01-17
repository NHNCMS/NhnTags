using System.Linq.Expressions;

namespace NhnTags.DataModel.Abstracts;

public interface IPersister<T> where T : ModelBase
{
    Task<T> GetById(string id);
    Task Insert(T dtoToInsert);
    Task Replace(T dtoToUpdate);
    Task UpdateOne(string id, Dictionary<string, object> propertiesToUpdate);

    Task<string> Delete(string id);
    Task DeleteMany(Expression<Func<T, bool>> filter);
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>>? filter = null);

    T ConstructEntity();
}