using System;

namespace Thunder.Platform.Core.Helpers
{
    public static class GuidGenerator
    {
        /// <summary>Returns a string representation of the value of this <see cref="T:System.Guid" /> instance, according to the provided format specifier.</summary>
        /// <param name="format">A single format specifier that indicates how to format the value of this <see cref="T:System.Guid" />. The <paramref name="format" /> parameter can be "N", "D", "B", "P", or "X". If <paramref name="format" /> is <see langword="null" /> or an empty string (""), "D" is used. </param>
        /// <returns>The value of this <see cref="T:System.Guid" />, represented as a series of lowercase hexadecimal digits in the specified format. </returns>
        /// <exception cref="T:System.FormatException">The value of <paramref name="format" /> is not <see langword="null" />, an empty string (""), "N", "D", "B", "P", or "X". </exception>
        public static string NewGuid(string format = "N")
        {
            return Guid.NewGuid().ToString(format);
        }
    }
}
