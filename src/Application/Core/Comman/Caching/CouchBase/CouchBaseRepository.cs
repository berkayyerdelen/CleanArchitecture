using System;
using System.Threading.Tasks;
using Core.Comman.Infrastructure;
using Couchbase;
using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using NLog.LayoutRenderers;

namespace Core.Comman.Caching.CouchBase
{
    public class CouchBaseRepository<TEntity> : ICouchBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly IBucket _bucket;

        public CouchBaseRepository(IBucketProvider bucketprovider) => _bucket = bucketprovider.GetBucket("mycache");

        public async Task<bool> Add(TEntity entity,string cachekey)
        {
            //CREATE INDEX ID on `mycache`(meta().id);
            var result =await _bucket.InsertAsync(new Document<dynamic>()
            {
                Id = cachekey,
                Content =  entity,
                //Expiry = 20000  //20 sec
                Expiry = 3000000,
            });
            
            return result.Success;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await _bucket.RemoveAsync(id);
            return result.Success;
        }

        public async Task<TEntity> GetByKey(string id)
        {
            var result = await _bucket.GetAsync<TEntity>(id);
            return result.Value;
        }

        public async Task<bool> IsExists(string id)
        {
            var result = await _bucket.ExistsAsync(id);
            return result;
        }

        public async Task<bool> Upsert(string id, TEntity entity)
        {
            var result = await _bucket.GetAsync<TEntity>(id);
            return result.Success;
        }
    }
}