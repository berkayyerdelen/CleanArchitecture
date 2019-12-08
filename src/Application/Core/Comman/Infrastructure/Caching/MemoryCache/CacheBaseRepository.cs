using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Comman.Extensions;
using Core.Comman.Interface.Caching;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Comman.Infrastructure.Caching.MemoryCache
{
    public class CacheBaseRepository : ICacheBaseRepository
    {
       
        private readonly IMemoryCache _cache;
        public CacheBaseRepository()
        {
            
            _cache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }

        public Task<T> Get<T>(string key)
        {
            return Task.FromResult(_cache.Get<T>(key));
        }

        public Task<object> Get(string key)
        {
            return Task.FromResult(_cache.Get(key));
        }

        public Task Add(string key, object data, int duration)
        {
            return Task.FromResult(_cache.Set(key, data, TimeSpan.FromMinutes(60)));
        }

        public Task<bool> IsAdd(string key)
        {
            return Task.FromResult(_cache.TryGetValue(key, out _));
        }

        public Task Remove(string key)
        {
            _cache.Remove(key);
           return Task.CompletedTask;
        }

        public Task RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(Microsoft.Extensions.Caching.Memory.MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition?.GetValue(_cache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
            }
            return Task.CompletedTask;
        }
    }
}