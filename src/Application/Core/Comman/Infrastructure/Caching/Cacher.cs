using System;
using System.Threading.Tasks;
using Core.Comman.Interface.Caching;
using Couchbase.Extensions.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Core.Comman.Infrastructure.Caching
{
    public class Cacher : ICaching
    {
        public IDistributedCache _cacher { get; set; }
        public Cacher(IDistributedCache cacher) => _cacher = cacher;

        public string GetCachedData(string cacheKey)
        {
            var cachedData = _cacher.Get<object>(cacheKey);
            return JsonConvert.SerializeObject(cachedData);

        }
        public void SetCache(string cacheKey, object obj)
        {
            _cacher.Set(cacheKey, obj, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) });
        }
    }
}