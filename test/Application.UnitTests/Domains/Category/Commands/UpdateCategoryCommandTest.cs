using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.Common;
using Core.Domains.Category.Commands.UpdateCategory;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Domains.Category.Commands
{
    public class UpdateCategoryCommandTest: CommandTestBase
    {
        [Fact]
        public async Task Handle_Update_Category()
        {
            var category = new UpdateCategoryCommand()
            {
                Description = "Notebook",
                Id = 1,
                CategoryName = "Laptop1"
            };
            var handler = new UpdateCategoryCommandHandler.Handler(_context);
            await handler.Handle(category, CancellationToken.None);
            var categoryName = _context.Categories.Find(1);
            categoryName.CategoryName.ShouldBe("Laptop1");
        }
    }
}