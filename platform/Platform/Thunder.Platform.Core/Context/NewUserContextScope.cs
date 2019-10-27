using System;

namespace Thunder.Platform.Core.Context
{
    /// <summary>
    /// Using this scope, we can create a new UserContext scope and it will be restored to the origin after disposed
    /// using(new NewUserContextScope()) {
    ///     // Do something here
    /// }.
    /// </summary>
    public class NewUserContextScope : IDisposable
    {
        private readonly UserContextContents _currentContext;

        public NewUserContextScope()
        {
            _currentContext = UserContext.Current.ExportContents();
            UserContext.Current.Clear();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (_currentContext != null)
            {
                UserContext.Current.ImportContents(_currentContext);
            }
        }
    }
}
