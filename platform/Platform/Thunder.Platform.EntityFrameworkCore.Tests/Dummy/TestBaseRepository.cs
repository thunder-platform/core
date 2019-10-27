using System;
using Thunder.Platform.Core.Domain.Entities;
using Thunder.Platform.Core.Domain.UnitOfWork.Abstractions;
using Thunder.Platform.EntityFrameworkCore.Repositories;

namespace Thunder.Platform.EntityFrameworkCore.Tests.Dummy
{
    public class TestBaseRepository<TEntity> : BaseEfCoreRepository<TestSqliteDbContext, TEntity>
        where TEntity : class, IEntity<Guid>
    {
        public TestBaseRepository(IUnitOfWorkManager unitOfWorkManager, IDbContextProvider<TestSqliteDbContext> dbContextProvider) : base(unitOfWorkManager, dbContextProvider)
        {
        }
    }
}
