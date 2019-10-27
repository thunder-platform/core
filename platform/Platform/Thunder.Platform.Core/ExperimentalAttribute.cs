using System;

namespace Thunder.Platform.Core
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class ExperimentalAttribute : Attribute
    {
        public ExperimentalAttribute(string description)
        {
            Description = description;
        }

        public string Description { get; }
    }
}
