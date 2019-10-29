using System.Threading;
using System.Threading.Tasks;
using Core.Domains.Category.Commands.CreateCategory;
using Core.Domains.Category.Commands.DeleteCategory;
using Core.Domains.Category.Commands.UpdateCategory;
using Core.Domains.Category.Queries.FindCategoryByName;
using Core.Domains.Category.Queries.GetCategoryList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WabApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<Unit> CreateCategory(CreateCategoryCommand request, CancellationToken ct)
        {
            return await _mediator.Send(request, ct);
        }

        [HttpDelete]
        public async Task<Unit> DeleteCategory(DeleteCategoryCommand request, CancellationToken ct)
        {
            return await _mediator.Send(request, ct);
        }

        [HttpPut]
        public async Task<Unit> UpdateCategory(UpdateCategoryCommand request, CancellationToken ct)
        {
            return await _mediator.Send(request, ct);
        }

        [HttpGet]
        public async Task<CategoryListViewModel> GetCategoryList(CancellationToken ct)
        {
            return await _mediator.Send(new GetCategoryListQuery(), ct);
        }

        [HttpGet]
        [Route("findbyname/{name}")]
        public async Task<FindCategoryByNameViewModel> GetCategoryInfo(string name, CancellationToken ct)
        {
            return await _mediator.Send(new FindCategoryByNameQuery(name), ct);
        }
    }
}