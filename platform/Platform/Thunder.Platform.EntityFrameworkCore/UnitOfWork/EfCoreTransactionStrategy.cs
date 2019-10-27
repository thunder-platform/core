using System.Collections.Generic;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Thunder.Platform.Core.Domain.UnitOfWork;
using Thunder.Platform.Core.Extensions;
using Thunder.Platform.EntityFrameworkCore.Extensions;

namespace Thunder.Platform.EntityFrameworkCore.UnitOfWork
{
    public class EfCoreTransactionStrategy : IEfCoreTransactionStrategy
    {
        public EfCoreTransactionStrategy()
        {
            ActiveTransactions = new Dictionary<string, ActiveTransactionInfo>();
        }

        protected IDictionary<string, ActiveTransactionInfo> ActiveTransactions { get; }

        protected UnitOfWorkOptions Options { get; private set; }

        public void InitOptions(UnitOfWorkOptions options)
        {
            Options = options;
        }

        public DbContext CreateDbContext<TDbContext>(string connectionString, IDbContextResolver dbContextResolver) where TDbContext : BaseThunderDbContext
        {
            DbContext dbContext;

            var activeTransaction = ActiveTransactions.GetOrDefault(connectionString);
            if (activeTransaction == null)
            {
                dbContext = dbContextResolver.Resolve<TDbContext>(connectionString);

                var dbTransaction = dbContext.Database.BeginTransaction((Options.IsolationLevel ?? IsolationLevel.ReadUncommitted).ToSystemDataIsolationLevel());
                activeTransaction = new ActiveTransactionInfo(dbTransaction, dbContext);
                ActiveTransactions[connectionString] = activeTransaction;
            }
            else
            {
                dbContext = dbContextResolver.Resolve<TDbContext>(
                    connectionString,
                    activeTransaction.DbContextTransaction.GetDbTransaction().Connection);

                if (dbContext.HasRelationalTransactionManager())
                {
                    dbContext.Database.UseTransaction(activeTransaction.DbContextTransaction.GetDbTransaction());
                }
                else
                {
                    dbContext.Database.BeginTransaction();
                }

                activeTransaction.AttendedDbContexts.Add(dbContext);
            }

            return dbContext;
        }

        public void Commit()
        {
            foreach (var activeTransaction in ActiveTransactions.Values)
            {
                activeTransaction.DbContextTransaction.Commit();

                foreach (var dbContext in activeTransaction.AttendedDbContexts)
                {
                    if (dbContext.HasRelationalTransactionManager())
                    {
                        // Relational databases use the shared transaction
                        continue;
                    }

                    dbContext.Database.CommitTransaction();
                }
            }
        }

        public void Dispose()
        {
            foreach (var activeTransaction in ActiveTransactions.Values)
            {
                activeTransaction.DbContextTransaction.Dispose();

                foreach (var attendedDbContext in activeTransaction.AttendedDbContexts)
                {
                    attendedDbContext.Dispose();
                }

                activeTransaction.StarterDbContext.Dispose();
            }

            ActiveTransactions.Clear();
        }
    }
}
