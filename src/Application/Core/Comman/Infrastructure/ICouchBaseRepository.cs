using System.Threading.Tasks;

namespace Core.Comman.Infrastructure
{
    public interface ICouchBaseRepository<TEntity>
    {
        Task<TEntity> GetById(string id);
        Task<bool> IsExists(string id);
        Task<bool> Add(TEntity entity);
        Task<bool> Upsert(string id, TEntity entity);
        Task<bool> Patch(string id, TEntity entity);
        Task<bool> Delete(string id);
    }
}