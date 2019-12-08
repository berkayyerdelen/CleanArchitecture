using System.Threading.Tasks;

namespace Core.Comman.Interface.Caching
{
    public interface ICacheBaseRepository
    {
        Task<T> Get<T>(string key);
        Task<object> Get(string key);
        Task Add(string key, object data, int duration);
        Task<bool> IsAdd(string key);
        Task Remove(string key);
        Task RemoveByPattern(string pattern);
    }
}