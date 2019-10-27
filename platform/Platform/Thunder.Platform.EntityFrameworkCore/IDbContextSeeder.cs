using Microsoft.EntityFrameworkCore;

namespace Thunder.Platform.EntityFrameworkCore
{
    public interface IDbContextSeeder
    {
        void Seed(ModelBuilder modelBuilder);
    }
}
