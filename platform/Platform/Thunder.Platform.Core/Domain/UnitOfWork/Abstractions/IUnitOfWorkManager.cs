using System.Transactions;

namespace Thunder.Platform.Core.Domain.UnitOfWork.Abstractions
{
    /// <summary>
    /// Unit of work manager.
    /// Used to begin and control a unit of work.
    /// </summary>
    public interface IUnitOfWorkManager
    {
        /// <summary>
        /// Gets currently active unit of work (or null if not exists).
        /// </summary>
        IUnitOfWork Current { get; }

        IUnitOfWorkCompleteHandle Begin();

        IUnitOfWorkCompleteHandle Begin(TransactionScopeOption scope);

        IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options);
    }
}
