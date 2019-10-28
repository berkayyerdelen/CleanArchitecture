using MediatR;

namespace Core.Domains.Category.Queries.FindCategoryByName
{
    public class FindCategoryByNameQuery:IRequest
    {
        public string CategoryName { get; set; }

       
    }
}