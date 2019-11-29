using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Core.Comman.Interface.Caching
{
    public interface ICaching
    {
        string GetCachedData(string cacheKey);
        void SetCache(string cacheKey, object obj);
    }
}