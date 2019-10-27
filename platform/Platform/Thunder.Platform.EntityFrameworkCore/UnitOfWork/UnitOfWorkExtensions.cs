using System;
using Thunder.Platform.Core.Domain.UnitOfWork.Abstractions;

namespace Thunder.Platform.EntityFrameworkCore.UnitOfWork
{
    public static class UnitOfWorkExtensions
    {
        /// <summary>
        /// Gets a DbContext as a part of active unit of work.
        /// This method can be called when current unit of work is an <see cref="EfCoreUnitOfWork"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of the DbContext.</typeparam>
        /// <param name="unitOfWork">Current (active) unit of work.</param>
        /// <param name="name">
        /// A custom name for the dbcontext to get a named dbcontext.
        /// If there is no dbcontext in this unit of work with given name, then a new one is created.
        /// </param>
        /// <returns>The dbcontext.</returns>
        public static TDbContext GetDbContext<TDbContext>(this IActiveUnitOfWork unitOfWork, string name = null)
            where TDbContext : BaseThunderDbContext
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException($"Please provide {nameof(unitOfWork)} before performing the database processing.");
            }

            if (!(unitOfWork is EfCoreUnitOfWork))
            {
                throw new ArgumentException($"{nameof(unitOfWork)} is not type of " + typeof(EfCoreUnitOfWork).FullName, nameof(unitOfWork));
            }

            return (unitOfWork as EfCoreUnitOfWork).GetOrCreateDbContext<TDbContext>(name);
        }
    }
}
