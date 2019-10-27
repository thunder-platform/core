using System;
using JetBrains.Annotations;

namespace Thunder.Platform.ThunderBus.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ConsumerAttribute : Attribute
    {
        public ConsumerAttribute([NotNull] string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Topic or exchange route key name.
        /// </summary>
        public string Name { get; }
    }
}
