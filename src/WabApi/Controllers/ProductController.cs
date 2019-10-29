﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Domains.Category.Commands.DeleteCategory;
using Core.Domains.Category.Commands.UpdateCategory;
using Core.Domains.Category.Queries.FindCategoryByName;
using Core.Domains.Product.Commands.CreateProduct;
using Core.Domains.Product.Commands.DeleteProduct;
using Core.Domains.Product.Commands.UpdateProduct;
using Core.Domains.Product.Queries.FindProductByName;
using Core.Domains.Product.Queries.GetProductList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WabApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<Unit> CreateProduct(CreateProductCommand request, CancellationToken ct)
        {
            return await _mediator.Send(request, ct);
        }

        [HttpDelete]
        public async Task<Unit> DeleteProduct(DeleteProductCommand request, CancellationToken ct)
        {
            return await _mediator.Send(request, ct);
        }

        [HttpPut]
        public async Task<Unit> UpdateProduct(UpdateProductCommand request, CancellationToken ct)
        {
            return await _mediator.Send(request, ct);
        }
        [HttpGet]
        [Route("findbyname/{name}")]
        public async Task<FindProductByNameViewModel> GetCategoryInfo(string name, CancellationToken ct)
        {
            return await _mediator.Send(new FindProductByNameQuery(name), ct);
        }

        [HttpGet]
        public async Task<ProductListViewModel> GetProductList(CancellationToken ct)
        {
            return await _mediator.Send(new GetProductListQuery(), ct);
        }
    }
}