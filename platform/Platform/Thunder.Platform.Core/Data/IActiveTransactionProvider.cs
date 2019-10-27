using System.Data;

namespace Thunder.Platform.Core.Data
{
    public interface IActiveTransactionProvider
    {
        /// <summary>
        /// Gets the active transaction or null if current UOW is not transactional.
        /// </summary>
        /// <param name="args">The args to get the active transaction.</param>
        /// <returns>The active transaction.</returns>
        IDbTransaction GetActiveTransaction(ActiveTransactionProviderArgs args);

        /// <summary>
        /// Gets the active database connection.
        /// </summary>
        /// <param name="args">The args to get the active transaction.</param>
        /// <returns>The active transaction.</returns>
        IDbConnection GetActiveConnection(ActiveTransactionProviderArgs args);
    }
}
