using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Interface
{
    public interface IApplicationDbContext
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,CancellationToken cancellationToken);
    }
}