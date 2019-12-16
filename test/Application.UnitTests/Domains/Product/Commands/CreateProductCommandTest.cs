using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.Common;
using Core.Domains.Product.Commands.CreateProduct;
using Xunit;

namespace Application.UnitTests.Domains.Product.Commands
{
    public class CreateProductCommandTest: CommandTestBase
    {
        [Fact]
        public async Task Handle_Create_Product()
        {
            var product = new CreateProductCommand()
            {
                ProductName = "Next Product",
                UnitsInStock = 10,
                UnitPrice = 15
            };
            var handler = new CreateProductCommand.Handler(_context);
            await handler.Handle(product, cancellationToken: CancellationToken.None);
            var count = _context.Products.Count();

            Assert.Equal(4,count);
        }
    }
}