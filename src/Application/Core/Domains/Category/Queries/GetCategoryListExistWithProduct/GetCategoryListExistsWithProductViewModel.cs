using System.Collections.Generic;
using MediatR;

namespace Core.Domains.Category.Queries.GetCategoryListExistWithProduct
{
    public class GetCategoryListExistsWithProductViewModel
    {
        public IList<GetCategoryListExistsWithProductLookupModel> CategoryListExistsWithProduct { get; set; }
        
    }
}