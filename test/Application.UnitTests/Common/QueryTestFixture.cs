using System;
using AutoMapper;
using Core.Comman.Interface.Caching;
using Core.Domains.Category.Queries.GetCategoryList;
using Persistence;
using Xunit;

namespace Application.UnitTests.Common
{
    public class QueryTestFixture:IDisposable
    {
        public ApplicationDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }
       
        public QueryTestFixture()
        {
            Context = ApplicationContextFactory.Create();
            Mapper = AutoMapperFactory.Create();
        }
       
        public void Dispose()
        {
            ApplicationContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
