using Microsoft.EntityFrameworkCore;
using Thunder.Platform.Core.Domain.UnitOfWork.Abstractions;

namespace Thunder.Platform.EntityFrameworkCore.Tests.Dummy
{
    public class TestSqliteDbContext : BaseThunderDbContext
    {
        private readonly IConnectionStringResolver _connectionStringResolver;

        public TestSqliteDbContext(IConnectionStringResolver connectionStringResolver)
        {
            _connectionStringResolver = connectionStringResolver;
        }

        public DbSet<EmployeeEntity> EmployeeEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (DbConnection != null)
            {
                optionsBuilder.UseSqlite(DbConnection);
            }
            else
            {
                optionsBuilder.UseSqlite(ChooseConnectionString());
            }

            base.OnConfiguring(optionsBuilder);
        }

        private string ChooseConnectionString()
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            // We want to explicit code here so please do not use ? :
            if (string.IsNullOrEmpty(ConnectionString))
            {
                return _connectionStringResolver.GetNameOrConnectionString(new ConnectionStringResolveArgs());
            }

            return ConnectionString;
        }
    }
}
