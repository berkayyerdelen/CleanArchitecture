using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.Common;
using Core.Domains.Product.Commands.DeleteProduct;
using Xunit;

namespace Application.UnitTests.Domains.Product.Commands
{
    public class DeleteProductCommandTest:CommandTestBase
    {
        [Theory]
        [InlineData(2)]
        public async Task Handle_Delete_Product(int numberOfProducts)
        {
            var product = new DeleteProductCommand()
            {
                Id = 1
            };
            var handler = new DeleteProductCommand.Handler(_context);
            await handler.Handle(product, cancellationToken: CancellationToken.None);
            var count = _context.Products.Count();
            Assert.Equal(count,numberOfProducts);
        }
    }
}