using System.Collections.Generic;

namespace Core.Domains.Category.Queries.GetCategoryList
{
    public class CategoryListViewModel
    {
        public IList<CategoryLookupModel> Categories { get; set; }
    }
}