using Thunder.Platform.Core.Domain.UnitOfWork.Abstractions;

namespace Thunder.Platform.EntityFrameworkCore.UnitOfWork
{
    /// <summary>
    /// Implements <see cref="IDbContextProvider{TDbContext}"/> that gets DbContext from
    /// active unit of work.
    /// </summary>
    /// <typeparam name="TDbContext">Type of the DbContext.</typeparam>
    public class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : BaseThunderDbContext
    {
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        public UnitOfWorkDbContextProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }

        public TDbContext GetDbContext()
        {
            return _currentUnitOfWorkProvider.Current.GetDbContext<TDbContext>();
        }
    }
}
