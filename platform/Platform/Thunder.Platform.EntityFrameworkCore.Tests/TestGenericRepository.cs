using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.Core.Domain.Repositories;
using Thunder.Platform.Core.Domain.UnitOfWork.Abstractions;
using Thunder.Platform.EntityFrameworkCore.Tests.Dummy;
using Xunit;

namespace Thunder.Platform.EntityFrameworkCore.Tests
{
    public class TestGenericRepository : BaseEntityFrameworkCoreTest
    {
        [Fact]
        public async Task Create_generic_repository_should_be_ok()
        {
            // Arrange
            const string testName = "test";
            var resolver = Provider.GetService<IDbContextResolver>();
            var uowManager = Provider.GetService<IUnitOfWorkManager>();
            var employeeRepository = Provider.GetService<IRepository<EmployeeEntity>>();

            // Act
            using (var uow = uowManager.Begin())
            {
                await employeeRepository.InsertAsync(new EmployeeEntity { Name = testName });

                await uow.CompleteAsync();
            }

            // Assert
            using (var context = resolver.Resolve<TestSqliteDbContext>(ConnectionStringFor("TestDb")))
            {
                var insertedEmployee = await context.EmployeeEntities.FirstOrDefaultAsync(b => b.Name.Equals(testName));
                Assert.NotNull(insertedEmployee);
                Assert.Equal(insertedEmployee.Name, testName);
            }
        }
    }
}
