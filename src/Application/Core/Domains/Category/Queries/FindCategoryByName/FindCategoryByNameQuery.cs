﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Category.Queries.FindCategoryByName
{
    public class FindCategoryByNameQuery:IRequest<FindCategoryByNameViewModel>
    {
        public string CategoryName { get; set; }

        public FindCategoryByNameQuery(string categoryName)
        {
            CategoryName = categoryName;
        }

       public class Handler:IRequestHandler<FindCategoryByNameQuery,FindCategoryByNameViewModel>
       {
           private readonly IApplicationDbContext _context;
           private readonly IMapper _mapper;
           public Handler(IApplicationDbContext context, IMapper mapper)
           {
               _mapper = mapper;
               _context = context;
           }
           public async Task<FindCategoryByNameViewModel> Handle(FindCategoryByNameQuery request, CancellationToken cancellationToken)
            {
               var entity = await _context.Set<Entities.Category>().ProjectTo<FindCategoryByNameLookupModel>(_mapper.ConfigurationProvider)
                   .SingleOrDefaultAsync(x => x.CategoryName == request.CategoryName,cancellationToken);
               return new FindCategoryByNameViewModel
               {
                   CategoryName = entity.CategoryName,
                   Descriptipn = entity.Descriptipn
               };
           }
       }
    }
}