using System;
using System.Security.Authentication;

namespace Thunder.Platform.Core.Exceptions
{
    public static class ThunderExceptionExtensions
    {
        public static ErrorInfo ToDisplayError(this GeneralException exception)
        {
            return new ErrorInfo
            {
                Code = nameof(GeneralException),
                Message = exception.Message
            };
        }

        public static ErrorInfo ToDisplayError<TGeneralException>(this TGeneralException exception) where TGeneralException : GeneralException
        {
            return new ErrorInfo
            {
                Code = exception.GetType().Name,
                Message = exception.Message
            };
        }

        public static ErrorInfo ToDisplayError(this Exception exception)
        {
            return new ErrorInfo
            {
                Code = exception.GetType().Name,
                Message = exception.Message
            };
        }

        public static ErrorInfo ToDisplayError(this AuthenticationException exception)
        {
            return new ErrorInfo
            {
                Code = exception.GetType().Name,
                Message = exception.Message
            };
        }
    }
}
