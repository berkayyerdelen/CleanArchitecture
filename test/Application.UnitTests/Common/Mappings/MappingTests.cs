using System;
using AutoMapper;
using Core.Domains.Category.Commands.CreateCategory;
using Core.Domains.Product.Queries.FindProductByName;
using Xunit;

namespace Application.UnitTests.Common.Mappings
{
    public class MappingTests : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests(MappingTestsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(FindProductByNameLookupModel), typeof(Entities.Product))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }
    }
}