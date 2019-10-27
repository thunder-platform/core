using System;
using System.Collections.Generic;
using System.Transactions;
using Thunder.Platform.Core.Application;
using Thunder.Platform.Core.Domain.Repositories;
using Thunder.Platform.Core.Domain.UnitOfWork.Abstractions;

namespace Thunder.Platform.Core.Domain.UnitOfWork
{
    internal class UnitOfWorkDefaultOptions : IUnitOfWorkDefaultOptions
    {
        public UnitOfWorkDefaultOptions()
        {
            IsTransactional = true;
            Scope = TransactionScopeOption.Required;
            IsTransactionScopeAvailable = true;

            ConventionalUowSelectors = new List<Func<Type, bool>>
            {
                type => typeof(IRepository).IsAssignableFrom(type) ||
                        typeof(IApplicationService).IsAssignableFrom(type)
            };
        }

        public TransactionScopeOption Scope { get; set; }

        public bool IsTransactional { get; set; }

        public bool IsTransactionScopeAvailable { get; set; }

        public TimeSpan? Timeout { get; set; }

        public IsolationLevel? IsolationLevel { get; set; }

        public List<Func<Type, bool>> ConventionalUowSelectors { get; }
    }
}
