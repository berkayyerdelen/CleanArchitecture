using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.Common;
using Core.Domains.Product.Commands.UpdateProduct;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Domains.Product.Commands
{
    public class UpdateProductCommandTest:CommandTestBase
    {
        [Theory]
        [InlineData("New Product Name",10,15)]
        
        public async Task Handle_Update_Product(string productName, short unitInStock, int unitPrice)
        {
            var product = new UpdateProductCommand()
            {   Id = 1,
                ProductName = productName,
                UnitsInStock = unitInStock,
                UnitPrice = unitPrice
            };
            var handler = new UpdateProductCommand.Handler(_context);
            await handler.Handle(product, cancellationToken: CancellationToken.None);
            var getProduct = _context.Products.FindAsync(1).Result;
            getProduct.ProductName.ShouldBe("New Product Name");
        }
    }
}