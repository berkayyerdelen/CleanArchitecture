﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.Common;
using Core.Domains.Category.Commands.CreateCategory;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Domains.Category.Commands
{
    public class CreateCategoryCommandTest: CommandTestBase
    {
        [Fact]
        public async Task Handle_Create_Category()
        {
            var command = new CreateCategoryCommand
            {
                CategoryName = "Laptop",
                Description = "MSI",
                Picture = null
            };

            var handler = new CreateCategoryCommand.Handler(_context);

            var result = await handler.Handle(command, CancellationToken.None);

            var count = _context.Categories.Count();

            count.ShouldBe(2);
        }
    }
}