namespace Thunder.Platform.Core.Dependency
{
    public interface IConventionalDependencyRegistrar
    {
        /// <summary>
        /// Registers types of given assembly by convention.
        /// </summary>
        /// <param name="context">Registration context.</param>
        void RegisterAssembly(IConventionalRegistrationContext context);
    }
}
