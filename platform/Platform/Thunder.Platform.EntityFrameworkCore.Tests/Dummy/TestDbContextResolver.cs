using System;
using System.Collections.Generic;
using System.Data.Common;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Thunder.Platform.EntityFrameworkCore.Tests.Dummy
{
    public class TestDbContextResolver : IDbContextResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public TestDbContextResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TDbContext Resolve<TDbContext>(string connectionString) where TDbContext : BaseThunderDbContext
        {
            var dbContext = ResolveDbContext<TDbContext>(connectionString, null);

            EnsureDatabaseCreated(dbContext);

            return dbContext;
        }

        public TDbContext Resolve<TDbContext>(string connectionString, DbConnection connection) where TDbContext : BaseThunderDbContext
        {
            var dbContext = ResolveDbContext<TDbContext>(connectionString, connection);

            EnsureDatabaseCreated(dbContext);

            return dbContext;
        }

        private TDbContext ResolveDbContext<TDbContext>([NotNull] string connectionString, [CanBeNull] DbConnection connection)
            where TDbContext : BaseThunderDbContext
        {
            var dbContext = _serviceProvider.GetService<TDbContext>();
            var seeder = _serviceProvider.GetService<IDbContextSeeder>();
            var mappings = _serviceProvider.GetService<IEnumerable<IEntityTypeMapper>>();
            dbContext.Mappings = mappings;
            dbContext.DbContextSeeder = seeder;
            dbContext.InitializeConnectionString(connectionString);

            if (connection != null)
            {
                dbContext.InitializeDbConnection(connection);
            }

            return dbContext;
        }

        private void EnsureDatabaseCreated<TDbContext>([NotNull] TDbContext dbContext) where TDbContext : BaseThunderDbContext
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
