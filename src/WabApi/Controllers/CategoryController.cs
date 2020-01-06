using System.Threading;
using System.Threading.Tasks;
using Core.Domains.Category.Commands.CreateCategory;
using Core.Domains.Category.Commands.DeleteCategory;
using Core.Domains.Category.Commands.UpdateCategory;
using Core.Domains.Category.Queries.FindCategoryByName;
using Core.Domains.Category.Queries.GetCategoryList;
using Core.Domains.Category.Queries.GetCategoryListExcept;
using Core.Domains.Category.Queries.GetCategoryListExistWithProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WabApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(IMediator mediator, ILogger<CategoryController> logger)
            => (_mediator, _logger) = (mediator, logger);

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<Unit> CreateCategory(CreateCategoryCommand request, CancellationToken ct)
            => await _mediator.Send(request, ct);

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<Unit> DeleteCategory(DeleteCategoryCommand request, CancellationToken ct)
            => await _mediator.Send(request, ct);

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<Unit> UpdateCategory(UpdateCategoryCommand request, CancellationToken ct)
             => await _mediator.Send(request, ct);

        [HttpGet]
        [AllowAnonymous]
        public async Task<CategoryListViewModel> GetCategoryList(CancellationToken ct)
            => await _mediator.Send(new GetCategoryListQuery(), ct);

        [HttpGet]
        [Route("findbyname/{name}")]
        [AllowAnonymous]
        public async Task<FindCategoryByNameViewModel> GetCategoryInfo(string name, CancellationToken ct)
            => await _mediator.Send(new FindCategoryByNameQuery(name), ct);

        [HttpGet]
        [Route("CategoryExistWithProduct")]
        [AllowAnonymous]
        public async Task<GetCategoryListExistsWithProductViewModel> CategoryExistWithProduct(CancellationToken ct)
            => await _mediator.Send(new GetCategoryListExistsWithProductQuery(), ct);

        [HttpGet]
        [Route("CategoryExceptProduct")]
        [AllowAnonymous]
        public async Task<GetCategoryListExceptViewModel> CategoryExceptProduct(CancellationToken ct)
            => await _mediator.Send(new GetCategoryListExceptThanExpectedQuery(), ct);

    }
}