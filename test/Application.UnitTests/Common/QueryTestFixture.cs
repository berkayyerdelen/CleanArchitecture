using System;
using AutoMapper;
using Core.Comman.Infrastructure.AutoMapper;
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

    [Collection("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
