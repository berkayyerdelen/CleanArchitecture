﻿using System;
using AutoMapper;
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
            Mapper = AutoMapperFactory.Create();
        }

        public void Dispose()
        {
            ApplicationContextFactory.Destroy(Context);
        }
        [CollectionDefinition("QueryCollection")]
        public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
    }
}