using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Product.Queries.SumOfProductsByCategory
{
    public class SumOfProductsByCategoryQuery: IRequest<List<SumOfProductViewModal>>
    {
        public class Handler:IRequestHandler<SumOfProductsByCategoryQuery,List<SumOfProductViewModal>>
        {
            public IApplicationDbContext _context { get; set; }
            public Handler(IApplicationDbContext context) 
                => _context = context;
            public async Task<List<SumOfProductViewModal>> Handle(SumOfProductsByCategoryQuery request, CancellationToken cancellationToken)
            {
               return await (from c in _context.Set<Entities.Category>()
                    join p in _context.Set<Entities.Product>() on
                        c.Id equals p.CategoryId
                    select new
                    {
                        p.UnitPrice,
                        c.CategoryName
                    }
                    into x
                    group x by new {x.CategoryName, x.UnitPrice}
                    into g
                    select new SumOfProductViewModal
                    {
                        SumOfProductsLookUp = new List<SumOfProductsLookUpModal>()
                        {
                            new SumOfProductsLookUpModal()
                            {
                                CategoryName = g.Key.CategoryName,
                                SumOfProducts = g.Sum(x => x.UnitPrice.Value),
                            }
                        }
                    }).ToListAsync(cancellationToken);
                
            }
        }
    }
}