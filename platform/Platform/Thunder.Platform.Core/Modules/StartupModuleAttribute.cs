using System;

namespace Thunder.Platform.Core.Modules
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class StartupModuleAttribute : Attribute
    {
    }
}
