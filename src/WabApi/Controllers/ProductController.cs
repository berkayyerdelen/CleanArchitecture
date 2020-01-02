using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domains.CustomerOperationClaim.Queries.GetCustomerOperationClaims;
using Core.Domains.Product.Commands.CreateProduct;
using Core.Domains.Product.Commands.DeleteProduct;
using Core.Domains.Product.Commands.UpdateProduct;
using Core.Domains.Product.Queries.FindProductByName;
using Core.Domains.Product.Queries.GetProductList;
using Core.Domains.Product.Queries.GetProductsByCategory;
using Core.Domains.Product.Queries.SumOfProductsByCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WabApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<Unit> CreateProduct(CreateProductCommand request, CancellationToken ct)
            => await _mediator.Send(request, ct);


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<Unit> DeleteProduct(DeleteProductCommand request, CancellationToken ct)
            => await _mediator.Send(request, ct);


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<Unit> UpdateProduct(UpdateProductCommand request, CancellationToken ct)
            => await _mediator.Send(request, ct);


        [HttpGet]
        [Route("findbyname/{name}")]
        [AllowAnonymous]
        public async Task<FindProductByNameViewModel> GetCategoryInfo(string name, CancellationToken ct)
            => await _mediator.Send(new FindProductByNameQuery(name), ct);


        [HttpGet]
        [AllowAnonymous]
        public async Task<ProductListViewModel> GetProductList(CancellationToken ct)
            => await _mediator.Send(new GetProductListQuery(), ct);

        [HttpGet]
        [AllowAnonymous]
        [Route("getProductsbyCategory")]
        public async Task<List<GetProductsByCategoryListViewModel>> GetProductsByCategory(CancellationToken ct)
            => await _mediator.Send(new GetProductsByCategoryQuery(), ct);

        [HttpGet]
        [AllowAnonymous]
        [Route("SumProductsPricebyCategory")]
        public async Task<List<SumOfProductViewModal>> SumProductsPriceByCategory(CancellationToken ct)
            => await _mediator.Send(new SumOfProductsByCategoryQuery(), ct);

    }
}