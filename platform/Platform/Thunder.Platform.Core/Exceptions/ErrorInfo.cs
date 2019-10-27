using System;
using System.Collections.Generic;

namespace Thunder.Platform.Core.Exceptions
{
    /// <summary>
    /// Represent the error model returning to the client.
    /// The idea of this design is came from: https://github.com/Microsoft/api-guidelines/blob/master/Guidelines.md#51-errors.
    /// <example>
    /// This sample shows how the <see cref="ErrorInfo"/> object look like in JSON.
    /// <code>
    /// {
    /// "error": {
    ///     "code": "BadArgument",
    ///     "message": "Previous passwords may not be reused",
    ///     "target": "password",
    ///     "innerError": {
    ///         "code": "PasswordError",
    ///         "innerError": {
    ///             "code": "PasswordDoesNotMeetPolicy",
    ///             "minLength": "6",
    ///             "maxLength": "64",
    ///             "characterTypes": ["lowerCase","upperCase","number","symbol"],
    ///             "minDistinctCharacterTypes": "2",
    ///             "innerError": {
    ///                 "code": "PasswordReuseNotAllowed"
    ///             }
    ///         }
    ///     }
    ///   }
    /// }.
    /// </code>
    /// </example>
    /// </summary>
    [Serializable]
    public class ErrorInfo
    {
        /// <summary>
        /// One of a server-defined set of error types.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// A human-readable representation of the error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The target of the error.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// An array of details about specific errors that led to this reported error.
        /// </summary>
        public List<ErrorInfo> Details { get; set; }
    }
}
