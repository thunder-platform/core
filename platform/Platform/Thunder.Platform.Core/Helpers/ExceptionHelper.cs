using System;

namespace Thunder.Platform.Core.Helpers
{
    public static class ExceptionHelper
    {
        public static T GetFirstExceptionOfType<T>(Exception root)
            where T : Exception
        {
            if (root == null)
            {
                return null;
            }

            var exception = root as T;
            return exception ?? GetFirstExceptionOfType<T>(root.InnerException);
        }
    }
}
