using System.Threading;
using System.Threading.Tasks;

namespace Thunder.Platform.Cqrs
{
    public interface IThunderCqrs
    {
        /// <summary>
        /// A Command is an imperative instruction to do something; it only has one handler. We will throw an error for multiple registered handlers of a command.
        /// </summary>
        /// <param name="command">The command object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task object.</returns>
        Task SendCommand(
            BaseThunderCommand command,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// To get data by conditions defined in query object.
        /// </summary>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="query">The query object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task object.</returns>
        Task<TResult> SendQuery<TResult>(
            BaseThunderQuery<TResult> query,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// An Event is a notification that something has happened; it has zero or more handlers.
        /// </summary>
        /// <param name="event">The event object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task object.</returns>
        Task SendEvent(
            BaseThunderEvent @event,
            CancellationToken cancellationToken = default);
    }
}
