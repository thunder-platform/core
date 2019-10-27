using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;

namespace Thunder.Platform.Core.Modules
{
    public class ThunderModuleInfo
    {
        public ThunderModuleInfo([NotNull] Type type, [NotNull] IThunderModule instance)
        {
            Guard.NotNull(type, nameof(type));
            Guard.NotNull(instance, nameof(instance));

            Type = type;
            Instance = instance;
            Assembly = Type.GetTypeInfo().Assembly;

            Dependencies = new List<ThunderModuleInfo>();
        }

        /// <summary>
        /// The assembly which contains the module definition.
        /// </summary>
        public Assembly Assembly { get; }

        public Type Type { get; }

        public IThunderModule Instance { get; }

        public List<ThunderModuleInfo> Dependencies { get; set; }

        public override string ToString()
        {
            return (Type.AssemblyQualifiedName ??
                    Type.FullName) ?? string.Empty;
        }
    }
}
