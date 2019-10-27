using Microsoft.EntityFrameworkCore;

namespace Persistence.MSSQL
{
    public class MSSQLApplicationDbContext :ApplicationDbContext
    {
        public MSSQLApplicationDbContext(DbContextOptions options) :base(options)
        {
            
        }
    }
}