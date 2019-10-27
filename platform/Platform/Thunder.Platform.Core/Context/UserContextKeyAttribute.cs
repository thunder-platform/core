using System;

namespace Thunder.Platform.Core.Context
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class UserContextKeyAttribute : Attribute
    {
        public const string ContextKeyPrefix = "Thunder-ContextKey-";

        private const string ContextKeyConvention = "Thunder-ContextKey-{0}";

        public static string ComputedContextKeyFor(string memberName)
        {
            return string.Format(ContextKeyConvention, memberName);
        }
    }
}
