using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.MSSQL
{
    public static class MSSQLServiceCollectionExtensions
    {
        public static IServiceCollection AddMssqlDbContext(
            this IServiceCollection serviceCollection,
            IConfiguration config = null)
        {
            serviceCollection.AddDbContext<ApplicationDbContext, MSSQLApplicationDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("ApplicationDbContext"), b => b.MigrationsAssembly("Persistence.MSSQL"));
            });
            return serviceCollection;
        }
    }
}