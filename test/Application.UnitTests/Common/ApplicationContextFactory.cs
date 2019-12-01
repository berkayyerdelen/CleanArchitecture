using System;
using Entities;
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

            context.Categories.AddRange(new[]
            {
                new Entities.Category(){CategoryName = "TAK",Description = "TAK",Id = 1},
                new Entities.Category(){CategoryName = "JA", Description = "JA", Id = 2},
                new Entities.Category(){CategoryName = "DA", Description = "DA", Id = 3}
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