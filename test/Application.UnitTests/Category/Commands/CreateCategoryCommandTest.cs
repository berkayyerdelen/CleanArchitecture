using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.Common;
using Core.Domains.Category.Commands.CreateCategory;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Category.Commands
{
    public class CreateCategoryCommandTest: CommandTestBase
    {
        [Fact]
        public async Task Handle_ShouldPersistTodoItem()
        {
            var command = new CreateCategoryCommand
            {
                CategoryName = "Laptop",
                Description = "MSI",
                Picture = null
            };

            var handler = new CreateCategoryCommand.Handler(_context);

            var result = await handler.Handle(command, CancellationToken.None);

            var entity = _context.Categories.Find(result);

            entity.ShouldNotBeNull();
            entity.CategoryName.ShouldBe(command.CategoryName);
        }
    }
}