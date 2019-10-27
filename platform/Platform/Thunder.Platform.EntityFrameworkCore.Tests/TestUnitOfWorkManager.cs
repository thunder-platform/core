using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.Core.Domain.UnitOfWork.Abstractions;
using Xunit;

namespace Thunder.Platform.EntityFrameworkCore.Tests
{
    public class TestUnitOfWorkManager : BaseEntityFrameworkCoreTest
    {
        [Fact]
        public void Create_unit_of_work_should_ok()
        {
            var uowManager = Provider.GetService<IUnitOfWorkManager>();
            using (var uow = uowManager.Begin())
            {
                uow.Complete();
            }
        }

        [Fact]
        public void Create_inner_unit_of_work_should_ok()
        {
            var uowManager = Provider.GetService<IUnitOfWorkManager>();
            using (var uow = uowManager.Begin())
            {
                using (var innerUow = uowManager.Begin())
                {
                    innerUow.Complete();
                }

                uow.Complete();
            }
        }
    }
}
