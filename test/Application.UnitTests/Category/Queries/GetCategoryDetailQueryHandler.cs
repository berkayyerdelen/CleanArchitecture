using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.Common;
using AutoMapper;
using Core.Domains.Category.Queries.GetCategoryList;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Category.Queries
{
    [Collection("QueryCollection")]
    public class GetCategoryDetailQueryHandler
    {
        public readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;


        public GetCategoryDetailQueryHandler(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }
        [Fact]
        public async Task Get_Category_List()
        {
            var sut = new GetCategoryListQuery.Handler(_context, _mapper);
            var result = await sut.Handle(new GetCategoryListQuery(), CancellationToken.None);
            result.ShouldBeOfType<CategoryListViewModel>();
            result.Categories.Count.ShouldBe(21);
        }
    }
}