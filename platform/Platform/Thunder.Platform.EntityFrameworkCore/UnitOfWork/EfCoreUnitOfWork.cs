using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Thunder.Platform.Core.Domain.UnitOfWork;
using Thunder.Platform.Core.Domain.UnitOfWork.Abstractions;
using Thunder.Platform.Core.Extensions;
using Thunder.Platform.EntityFrameworkCore.Extensions;

namespace Thunder.Platform.EntityFrameworkCore.UnitOfWork
{
    public class EfCoreUnitOfWork : BaseUnitOfWork
    {
        private readonly IDbContextResolver _dbContextResolver;
        private readonly IEfCoreTransactionStrategy _transactionStrategy;

        public EfCoreUnitOfWork(
            IDbContextResolver dbContextResolver,
            IEfCoreTransactionStrategy transactionStrategy,
            IConnectionStringResolver connectionStringResolver,
            IUnitOfWorkDefaultOptions defaultOptions)
            : base(connectionStringResolver, defaultOptions)
        {
            _dbContextResolver = dbContextResolver;
            _transactionStrategy = transactionStrategy;
            ActiveDbContexts = new Dictionary<string, DbContext>();
        }

        protected IDictionary<string, DbContext> ActiveDbContexts { get; }

        public virtual TDbContext GetOrCreateDbContext<TDbContext>(string name = null)
            where TDbContext : BaseThunderDbContext
        {
            var connectionStringResolveArgs = new ConnectionStringResolveArgs
            {
                ["DbContextType"] = typeof(TDbContext)
            };
            var connectionString = ResolveConnectionString(connectionStringResolveArgs);

            var dbContextKey = typeof(TDbContext).FullName + "#" + connectionString;
            if (name != null)
            {
                dbContextKey += "#" + name;
            }

            if (!ActiveDbContexts.TryGetValue(dbContextKey, out var dbContext))
            {
                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                if (Options.IsTransactional == true)
                {
                    dbContext = _transactionStrategy.CreateDbContext<TDbContext>(connectionString, _dbContextResolver);
                }
                else
                {
                    dbContext = _dbContextResolver.Resolve<TDbContext>(connectionString, null);
                }

                if (Options.Timeout.HasValue &&
                    dbContext.Database.IsRelational() &&
                    !dbContext.Database.GetCommandTimeout().HasValue)
                {
                    dbContext.Database.SetCommandTimeout(Options.Timeout.Value.TotalSeconds.To<int>());
                }

                ActiveDbContexts[dbContextKey] = dbContext;
            }

            return (TDbContext)dbContext;
        }

        public override void SaveChanges()
        {
            foreach (var dbContext in GetAllActiveDbContexts())
            {
                SaveChangesInDbContext(dbContext);
            }
        }

        public override async Task SaveChangesAsync()
        {
            foreach (var dbContext in GetAllActiveDbContexts())
            {
                await SaveChangesInDbContextAsync(dbContext);
            }
        }

        protected override void BeginUow()
        {
            if (Options.IsTransactional == true)
            {
                _transactionStrategy.InitOptions(Options);
            }
        }

        protected override void CompleteUow()
        {
            SaveChanges();
            CommitTransaction();
        }

        protected override async Task CompleteUowAsync()
        {
            await SaveChangesAsync();
            CommitTransaction();
        }

        protected override void DisposeUow()
        {
            if (Options.IsTransactional == true)
            {
                _transactionStrategy.Dispose();
            }
            else
            {
                foreach (var context in GetAllActiveDbContexts())
                {
                    context.Dispose();
                }
            }

            ActiveDbContexts.Clear();
        }

        protected virtual void SaveChangesInDbContext(DbContext dbContext)
        {
            dbContext.SaveChanges();
        }

        protected virtual async Task SaveChangesInDbContextAsync(DbContext dbContext)
        {
            await dbContext.SaveChangesAsync();
        }

        private void CommitTransaction()
        {
            if (Options.IsTransactional == true)
            {
                _transactionStrategy.Commit();
            }
        }

        // ReSharper disable once ReturnTypeCanBeEnumerable.Local
        private IReadOnlyList<DbContext> GetAllActiveDbContexts()
        {
            return ActiveDbContexts.Values.ToImmutableList();
        }
    }
}
