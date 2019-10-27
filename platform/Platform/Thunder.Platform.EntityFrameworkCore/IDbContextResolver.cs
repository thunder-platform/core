using System.Data.Common;
using JetBrains.Annotations;

namespace Thunder.Platform.EntityFrameworkCore
{
    public interface IDbContextResolver
    {
        TDbContext Resolve<TDbContext>([NotNull] string connectionString) where TDbContext : BaseThunderDbContext;

        TDbContext Resolve<TDbContext>([NotNull] string connectionString, DbConnection connection) where TDbContext : BaseThunderDbContext;
    }
}
