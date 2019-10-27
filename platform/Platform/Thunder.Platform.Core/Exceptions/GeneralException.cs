using System;
using System.Runtime.Serialization;

namespace Thunder.Platform.Core.Exceptions
{
    /// <summary>
    /// This is the most abstract exception that the framework can throw. This is a very simple exception class for AW application.
    /// The principle is to have layers of exception with specific purpose.
    /// </summary>
    public class GeneralException : Exception
    {
        public GeneralException()
        {
        }

        public GeneralException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        public GeneralException(string message)
            : base(message)
        {
        }

        public GeneralException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
