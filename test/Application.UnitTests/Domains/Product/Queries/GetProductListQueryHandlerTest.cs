using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.Common;
using AutoMapper;
using Core.Domains.Product.Queries.GetProductList;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Domains.Product.Queries
{
    [Collection("QueryCollection")]
    public class GetProductListQueryHandlerTest
    {
        public readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetProductListQueryHandlerTest(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }
        [Fact]
        public async Task Get_Product_List_Count()
        {
            var sut = new GetProductListQuery.Handler(_context, _mapper);
            var result = await sut.Handle(new GetProductListQuery(), CancellationToken.None);
            //result.ShouldBeOfType<CategoryListViewModel>();
            result.Products.Count.ShouldBe(3);
        }
    }
}