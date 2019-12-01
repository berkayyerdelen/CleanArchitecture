using System.Threading.Tasks;

namespace Core.Comman.Infrastructure
{
    public interface ICouchBaseRepository<TEntity>
    {
        Task<TEntity> GetByKey(string id);
        Task<bool> IsExists(string id);
        Task<bool> Add(TEntity entity, string cacheKey);
        Task<bool> Upsert(string id, TEntity entity);
        Task<bool> Delete(string id);
    }
}