using System.Linq.Expressions;

namespace Context.Repositories
{
    public interface IMongoRepository<TEntity> where TEntity : MongoDocument
    {
        IEnumerable<TEntity> FilterBy(
            Expression<Func<TEntity, bool>> filterExpression);

        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression);

        Task<TEntity> FindByIdAsync(string id);

        Task<TEntity> InsertOneAsync(TEntity document);
        Task InsertManyAsync(IEnumerable<TEntity> documents);

        Task<TEntity> UpdateOneAsync(TEntity document);

        Task<TEntity> InsertOrUpdateOneAsync(TEntity document);

        Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression);

        Task DeleteByIdAsync(string id);

        Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression);
    }
}