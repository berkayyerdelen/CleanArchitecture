using System;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UnitTests.Common
{
    public class ApplicationContextFactory
    {
        public static ApplicationDbContext Create()
        {
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();

            context.Products.AddRange(new[]
            {
                new Entities.Product() {Id = 1,ProductName = "MSI",UnitPrice = 3000},
                new Entities.Product() {Id = 2,ProductName = "Dell",UnitPrice = 2000},
                new Entities.Product() {Id = 3,ProductName = "Hp",UnitPrice = 1000},
            });
            context.SaveChanges();
            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}