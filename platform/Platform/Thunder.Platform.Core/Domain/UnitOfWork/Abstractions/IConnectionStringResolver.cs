namespace Thunder.Platform.Core.Domain.UnitOfWork.Abstractions
{
    /// <summary>
    /// Used to get connection string when a database connection is needed.
    /// </summary>
    public interface IConnectionStringResolver
    {
        /// <summary>
        /// Gets a connection string name (in config file) or a valid connection string.
        /// </summary>
        /// <param name="args">Arguments that can be used while resolving connection string.</param>
        /// <returns>The name or connection string.</returns>
        string GetNameOrConnectionString(ConnectionStringResolveArgs args);
    }
}
