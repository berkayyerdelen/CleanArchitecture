using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Core.Comman.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Product.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryQuery : IRequest<List<GetProductsByCategoryListViewModel>>
    {

        public class Handler : IRequestHandler<GetProductsByCategoryQuery, List<GetProductsByCategoryListViewModel>>
        {
            public IApplicationDbContext _context { get; set; }
            public Handler(IApplicationDbContext context) => _context = context;

            public async Task<List<GetProductsByCategoryListViewModel>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
            {


                //var model = _context.Set<Entities.Product>()
                //    .GroupBy(c => new {c.CategoryId})
                //    .Select(c => new GetProductsByCategoryLookUpModel
                //        {CategoryName = c.Key.CategoryId.ToString(), ProductCount = c.Count()}).ToList();
                

                var result = await (_context.Set<Entities.Category>()
                    .Join(_context.Set<Entities.Product>(), c => c.Id, p => p.CategoryId, (c, p) => new {c, p})
                    .GroupBy(t => t.c.CategoryName, t => t.c)
                    .Select(r => new GetProductsByCategoryListViewModel
                    {
                        ListOfCategoryModel = new List<GetProductsByCategoryLookUpModel>()
                        {
                            new GetProductsByCategoryLookUpModel()
                            {
                                CategoryName = r.Key, ProductCount = r.Count()
                            }
                        }
                    })).ToListAsync(CancellationToken.None);
               
              
                return result;
            }


        }
    }
}