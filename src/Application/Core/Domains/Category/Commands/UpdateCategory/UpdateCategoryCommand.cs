using MediatR;

namespace Core.Domains.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommand:IRequest
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Pictures { get; set; }
    }
}