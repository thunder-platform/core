using System;
using System.Collections.Generic;
using System.Transactions;

namespace Thunder.Platform.Core.Domain.UnitOfWork.Abstractions
{
    /// <summary>
    /// Used to get/set default options for a unit of work.
    /// </summary>
    public interface IUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// Scope option.
        /// </summary>
        TransactionScopeOption Scope { get; set; }

        /// <summary>
        /// Should unit of works be transactional.
        /// Default: true.
        /// </summary>
        bool IsTransactional { get; set; }

        /// <summary>
        /// A boolean value indicates that System.Transactions.TransactionScope is available for current application.
        /// Default: true.
        /// </summary>
        bool IsTransactionScopeAvailable { get; set; }

        /// <summary>
        /// Gets/sets a timeout value for unit of works.
        /// </summary>
        TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Gets/sets isolation level of transaction.
        /// This is used if <see cref="IsTransactional"/> is true.
        /// </summary>
        IsolationLevel? IsolationLevel { get; set; }

        List<Func<Type, bool>> ConventionalUowSelectors { get; }
    }
}
