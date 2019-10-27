using System;

namespace Thunder.Platform.Core.Exceptions
{
    public class BusinessLogicException : GeneralException
    {
        public BusinessLogicException()
        {
        }

        public BusinessLogicException(string message)
            : base(message)
        {
        }

        public BusinessLogicException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
