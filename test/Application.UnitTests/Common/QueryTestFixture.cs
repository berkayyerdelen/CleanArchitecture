using System;
using AutoMapper;
using Core.Comman.Infrastructure.AutoMapper;
using Persistence;
using Xunit;

namespace Application.UnitTests.Common
{
    public class QueryTestFixture:IDisposable
    {
        public ApplicationDbContext Context { get; set; }
        public IMapper Mapper { get; set; }

     
        public QueryTestFixture()
        {
            Context = ApplicationContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }
        public void Dispose()
        {
            ApplicationContextFactory.Destroy(Context);
        }
        [CollectionDefinition("QueryCollection")]
        public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
    }
}