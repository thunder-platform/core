using Microsoft.EntityFrameworkCore;

namespace Thunder.Platform.EntityFrameworkCore
{
    public interface IDbContextProvider<out TDbContext>
        where TDbContext : DbContext
    {
        TDbContext GetDbContext();
    }
}
