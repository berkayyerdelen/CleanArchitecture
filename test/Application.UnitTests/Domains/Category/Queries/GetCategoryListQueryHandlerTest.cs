using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.Common;
using AutoMapper;
using Core.Comman.Interface.Caching;
using Core.Domains.Category.Queries.GetCategoryList;
using Core.Domains.Product.Queries.GetProductList;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Domains.Category.Queries
{
    [Collection("QueryCollection")]
    public class GetCategoryListQueryHandlerTest
    {
        public readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICouchBaseRepository<CategoryListViewModel> _couchBaseRepository;

        public GetCategoryListQueryHandlerTest(QueryTestFixture fixture,
            ICouchBaseRepository<CategoryListViewModel> couchBaseRepository)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
            _couchBaseRepository = couchBaseRepository;
        }
        [Fact]
        public async Task Get_Category_List()
        {
            var sut = new GetCategoryListQuery.Handler(_context, _mapper, _couchBaseRepository);
            var result = await sut.Handle(new GetCategoryListQuery(), CancellationToken.None);
            result.ShouldBeOfType<CategoryListViewModel>();
            result.Categories.Count.ShouldBe(0);
        }
    }
}