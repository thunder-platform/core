using System;

namespace Thunder.Platform.Core.Domain.UnitOfWork.Abstractions
{
    /// <summary>
    /// Used as event arguments on <see cref="IActiveUnitOfWork.Failed"/> event.
    /// </summary>
    public class UnitOfWorkFailedEventArgs : EventArgs
    {
        public UnitOfWorkFailedEventArgs(Exception exception)
        {
            Exception = exception;
        }

        public Exception Exception { get; private set; }
    }
}
