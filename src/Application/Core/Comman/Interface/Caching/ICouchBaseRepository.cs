using System.Threading.Tasks;

namespace Core.Comman.Interface.Caching
{
    public interface ICouchBaseRepository<TEntity>
    {
        Task<TEntity> GetByKey(string cachekey);
        Task<bool> IsExists(string cachekey);
        Task<bool> Add(TEntity entity, string cacheKey);
        Task Upsert(string cachekey, TEntity entity);
        Task<bool> Delete(string cachekey);
    }
}