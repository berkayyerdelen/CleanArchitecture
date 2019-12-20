using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.Common;
using Core.Domains.Category.Commands.DeleteCategory;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Domains.Category.Commands
{
    public class DeleteCategoryCommandTest: CommandTestBase
    {
        [Fact]
        public async Task Handle_Delete_Category()
        {
            var command = new DeleteCategoryCommand
            {
                Id = 1,
            };
            var handler = new DeleteCategoryCommand.Handler(_context);
            var result = await handler.Handle(command, cancellationToken: CancellationToken.None);
            var count = _context.Categories.Count();
            count.ShouldBe(0);
        }
    }
}