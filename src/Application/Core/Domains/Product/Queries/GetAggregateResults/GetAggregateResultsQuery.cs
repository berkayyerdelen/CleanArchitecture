using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Interface;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Product.Queries.GetAggregateResults
{
    public class GetAggregateResultsQuery : IRequest<List<GetAggregateResultsViewModel>>
    {
        public class Handler : IRequestHandler<GetAggregateResultsQuery, List<GetAggregateResultsViewModel>>
        {
            public IApplicationDbContext _context { get; set; }
            public Handler(IApplicationDbContext context)
                => _context = context;

            public async Task<List<GetAggregateResultsViewModel>> Handle(GetAggregateResultsQuery request, CancellationToken cancellationToken)
            {
              return await (from c in _context.Set<Entities.Category>()
                  join p in _context.Set<Entities.Product>() on
                      c.Id equals p.CategoryId
                      where c.Id> 5
                  select new
                  {
                      p.UnitPrice,
                      c.CategoryName
                  }
                  into x
                  group x by new { x.CategoryName }
                  into g
                  select new GetAggregateResultsViewModel
                  {
                      GetAggregateResultsLookUpModel = new List<GetAggregateResultsLookUpModel>()
                      {
                        new GetAggregateResultsLookUpModel()
                        {
                            CategoryName = g.Key.CategoryName,
                            SumPrice = g.Sum(x=>x.UnitPrice.Value),
                            AvaragePrice = g.Average(x=>x.UnitPrice.Value),
                            MaxPrice = g.Max(x=>x.UnitPrice.Value)
                        }
                      }
                  }).ToListAsync(cancellationToken);


   
            }
        }
    }
}