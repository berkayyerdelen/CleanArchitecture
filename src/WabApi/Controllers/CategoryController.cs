using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Domains.Category.Commands.CreateCategory;
using Core.Domains.Category.Commands.DeleteCategory;
using Core.Domains.Category.Commands.UpdateCategory;
using Core.Domains.Category.Queries.FindCategoryByName;
using Core.Domains.Category.Queries.GetCategoryList;
using Couchbase.Extensions.Caching;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace WabApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoryController> _logger;
        private readonly IDistributedCache _cache;
        public CategoryController(IMediator mediator,ILogger<CategoryController> logger, IDistributedCache cache)
            => (_mediator,_logger,_cache) = (mediator,logger,cache);


        [HttpPost]
        public async Task<Unit> CreateCategory(CreateCategoryCommand request, CancellationToken ct)
        { 
            _logger.LogInformation("CreateCategoy",request.CategoryName);
            return await _mediator.Send(request, ct);
        }


        [HttpDelete]
        public async Task<Unit> DeleteCategory(DeleteCategoryCommand request, CancellationToken ct)
            => await _mediator.Send(request, ct);


        [HttpPut]
        public async Task<Unit> UpdateCategory(UpdateCategoryCommand request, CancellationToken ct)
             => await _mediator.Send(request, ct);


        [HttpGet]
        public async Task<CategoryListViewModel> GetCategoryList(CancellationToken ct)
        {
            var message = _cache.GetString("CacheCategories");
            if (message is null)
            {
                var categorylist = await _mediator.Send(new GetCategoryListQuery(), ct);
                _cache.Set("CacheCategories", categorylist,new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                });
            }

           
            return null;
        }


        [HttpGet]
        [Route("findbyname/{name}")]
        public async Task<FindCategoryByNameViewModel> GetCategoryInfo(string name, CancellationToken ct)
            => await _mediator.Send(new FindCategoryByNameQuery(name), ct);

    }
}