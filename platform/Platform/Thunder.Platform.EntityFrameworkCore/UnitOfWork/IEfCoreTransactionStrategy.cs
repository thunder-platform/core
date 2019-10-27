using Microsoft.EntityFrameworkCore;
using Thunder.Platform.Core.Domain.UnitOfWork;

namespace Thunder.Platform.EntityFrameworkCore.UnitOfWork
{
    public interface IEfCoreTransactionStrategy
    {
        void InitOptions(UnitOfWorkOptions options);

        DbContext CreateDbContext<TDbContext>(string connectionString, IDbContextResolver dbContextResolver)
            where TDbContext : BaseThunderDbContext;

        void Commit();

        void Dispose();
    }
}
