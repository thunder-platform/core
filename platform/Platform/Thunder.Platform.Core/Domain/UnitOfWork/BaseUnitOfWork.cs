using System;
using System.Threading.Tasks;
using Thunder.Platform.Core.Domain.UnitOfWork.Abstractions;
using Thunder.Platform.Core.Exceptions;
using Thunder.Platform.Core.Extensions;

namespace Thunder.Platform.Core.Domain.UnitOfWork
{
    public abstract class BaseUnitOfWork : IUnitOfWork
    {
        private readonly IConnectionStringResolver _connectionStringResolver;

        private bool _isBeginCalledBefore;
        private bool _isCompleteCalledBefore;
        private Exception _exception;
        private bool _succeed;

        protected BaseUnitOfWork(
            IConnectionStringResolver connectionStringResolver,
            IUnitOfWorkDefaultOptions defaultOptions)
        {
            _connectionStringResolver = connectionStringResolver;
            Id = Guid.NewGuid().ToString("N");
            DefaultOptions = defaultOptions;
        }

        /// <inheritdoc/>
        public event EventHandler Completed;

        /// <inheritdoc/>
        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <inheritdoc/>
        public event EventHandler Disposed;

        public string Id { get; }

        public IUnitOfWork Outer { get; set; }

        public UnitOfWorkOptions Options { get; private set; }

        public bool IsDisposed { get; private set; }

        protected IUnitOfWorkDefaultOptions DefaultOptions { get; }

        public void Begin(UnitOfWorkOptions options)
        {
            Guard.NotNull(options, nameof(options));

            PreventMultipleBegin();

            // TODO: Do not set options like that, instead make a copy?
            Options = options;

            BeginUow();
        }

        /// <inheritdoc/>
        public void Complete()
        {
            PreventMultipleComplete();

            try
            {
                CompleteUow();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task CompleteAsync()
        {
            PreventMultipleComplete();

            try
            {
                await CompleteUowAsync();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (!_isBeginCalledBefore || IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (!_succeed)
            {
                OnFailed(_exception);
            }

            DisposeUow();
            OnDisposed();
        }

        public abstract void SaveChanges();

        public abstract Task SaveChangesAsync();

        /// <summary>
        /// Can be implemented by derived classes to start UOW.
        /// </summary>
        protected virtual void BeginUow()
        {
        }

        protected abstract void CompleteUow();

        protected abstract Task CompleteUowAsync();

        protected abstract void DisposeUow();

        /// <summary>
        /// Called to trigger <see cref="Completed"/> event.
        /// </summary>
        protected virtual void OnCompleted()
        {
            Completed.InvokeSafely(this);
        }

        /// <summary>
        /// Called to trigger <see cref="Failed"/> event.
        /// </summary>
        /// <param name="exception">Exception that cause failure.</param>
        protected virtual void OnFailed(Exception exception)
        {
            Failed.InvokeSafely(this, new UnitOfWorkFailedEventArgs(exception));
        }

        /// <summary>
        /// Called to trigger <see cref="Disposed"/> event.
        /// </summary>
        protected virtual void OnDisposed()
        {
            Disposed.InvokeSafely(this);
        }

        protected virtual string ResolveConnectionString(ConnectionStringResolveArgs args)
        {
            return _connectionStringResolver.GetNameOrConnectionString(args);
        }

        private void PreventMultipleBegin()
        {
            if (_isBeginCalledBefore)
            {
                throw new GeneralException("This unit of work has started before. Can not call Start method more than once.");
            }

            _isBeginCalledBefore = true;
        }

        private void PreventMultipleComplete()
        {
            if (_isCompleteCalledBefore)
            {
                throw new GeneralException("Complete is called before!");
            }

            _isCompleteCalledBefore = true;
        }
    }
}
