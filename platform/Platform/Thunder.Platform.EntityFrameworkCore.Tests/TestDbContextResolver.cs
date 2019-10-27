using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.EntityFrameworkCore.Tests.Dummy;
using Xunit;

namespace Thunder.Platform.EntityFrameworkCore.Tests
{
    public class TestDbContextResolver : BaseEntityFrameworkCoreTest
    {
        [Fact]
        public async Task Create_db_should_be_ok()
        {
            // Arrange
            var resolver = Provider.GetService<IDbContextResolver>();
            var dbContext = resolver.Resolve<TestSqliteDbContext>(ConnectionStringFor("TestDb"));
            var sampleEmployeeName = Guid.NewGuid().ToString("N");

            // Act
            await dbContext.EmployeeEntities.AddAsync(new EmployeeEntity { Name = sampleEmployeeName });
            await dbContext.SaveChangesAsync();
            var insertedEntity = await dbContext.EmployeeEntities.FirstOrDefaultAsync(p => p.Name.Equals(sampleEmployeeName));

            // Assert
            Assert.NotNull(insertedEntity);
        }

        [Fact]
        public void Create_transient_db_context_should_return_different_instances()
        {
            // Arrange
            // The DbContext is transient instance instead of Scoped by default.
            // services.AddEntityFrameworkSqlite().AddDbContext<TestSqliteDbContext>(ServiceLifetime.Transient);
            var resolver = Provider.GetService<IDbContextResolver>();
            var instance1 = resolver.Resolve<TestSqliteDbContext>(ConnectionStringFor("TestDb1"));
            var instance2 = resolver.Resolve<TestSqliteDbContext>(ConnectionStringFor("TestDb2"));

            // Act/Assert
            Assert.NotEqual(instance1.Id, instance2.Id);
        }
    }
}
