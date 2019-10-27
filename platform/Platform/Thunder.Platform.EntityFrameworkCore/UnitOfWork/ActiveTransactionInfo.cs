using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Thunder.Platform.EntityFrameworkCore.UnitOfWork
{
    public class ActiveTransactionInfo
    {
        public ActiveTransactionInfo(IDbContextTransaction dbContextTransaction, DbContext starterDbContext)
        {
            DbContextTransaction = dbContextTransaction;
            StarterDbContext = starterDbContext;

            AttendedDbContexts = new List<DbContext>();
        }

        public IDbContextTransaction DbContextTransaction { get; }

        public DbContext StarterDbContext { get; }

        public List<DbContext> AttendedDbContexts { get; }
    }
}
