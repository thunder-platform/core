using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Thunder.Platform.Core.Domain.UnitOfWork.Abstractions;
using Thunder.Platform.Core.Exceptions;

namespace Thunder.Platform.Core.Domain.UnitOfWork
{
    /// <summary>
    /// This handle is used for inner unit of work scopes.
    /// A inner unit of work scope actually uses outer unit of work scope
    /// and has no effect on <see cref="IUnitOfWorkCompleteHandle.Complete"/> call.
    /// But if it's not called, an exception is thrown at end of the UOW to rollback the UOW.
    /// </summary>
    internal class InnerUnitOfWorkCompleteHandle : IUnitOfWorkCompleteHandle
    {
        private volatile bool _isDisposed;

        public void Complete()
        {
        }

        public Task CompleteAsync()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
        }
    }
}
