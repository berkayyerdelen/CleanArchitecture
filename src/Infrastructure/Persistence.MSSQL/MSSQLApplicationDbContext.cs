using Microsoft.EntityFrameworkCore;

namespace Persistence.MSSQL
{
    public class MssqlApplicationDbContext :ApplicationDbContext
    {
        public MssqlApplicationDbContext(DbContextOptions options) :base(options)
        {
            
        }
    }
}