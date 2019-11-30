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

        public CouchBaseRepository(IBucketProvider bucketprovider) => _bucket = bucketprovider.GetBucket("dualist1224","lejyoner+9");

        public async Task<bool> Add(TEntity entity)
        {
            var result =await _bucket.InsertAsync(new Document<dynamic>()
            {
                Id = Guid.NewGuid().ToString(),
                Content =  entity,
                Expiry = 30
            });
            return result.Success;
        }

        public async Task<bool> Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<TEntity> GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> IsExists(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Patch(string id, TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Upsert(string id, TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}