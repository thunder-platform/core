using System.Threading.Tasks;
using Thunder.Platform.Core.Domain.UnitOfWork.Abstractions;

namespace Thunder.Platform.Core.Domain.UnitOfWork
{
    public sealed class NullUnitOfWork : BaseUnitOfWork
    {
        public NullUnitOfWork(IConnectionStringResolver connectionStringResolver, IUnitOfWorkDefaultOptions defaultOptions) : base(connectionStringResolver, defaultOptions)
        {
        }

        public override void SaveChanges()
        {
        }

        public override Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }

        protected override void CompleteUow()
        {
        }

        protected override Task CompleteUowAsync()
        {
            return Task.CompletedTask;
        }

        protected override void DisposeUow()
        {
        }
    }
}
