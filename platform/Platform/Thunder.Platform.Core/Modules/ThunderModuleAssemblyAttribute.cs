using System;

namespace Thunder.Platform.Core.Modules
{
    /// <summary>
    /// Specifies the given assembly is belong to thunder module system.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class ThunderModuleAssemblyAttribute : Attribute
    {
    }
}
