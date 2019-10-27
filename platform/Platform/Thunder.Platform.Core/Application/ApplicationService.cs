using System.Linq;
using System.Linq.Dynamic.Core;
using Thunder.Platform.Core.Application.Dtos;
using Thunder.Platform.Core.Extensions;

// ReSharper disable ConvertIfStatementToSwitchStatement
// ReSharper disable ConvertIfStatementToReturnStatement
namespace Thunder.Platform.Core.Application
{
    public abstract class ApplicationService : IApplicationService
    {
        protected virtual IQueryable<TEntity> ApplySorting<TEntity, TInput>(
            IQueryable<TEntity> query,
            TInput input,
            string defaultSortCriteria = "Id DESCENDING")
            where TEntity : class
        {
            // Try to sort query if available
            if (input is ISortedResultRequest sortInput)
            {
                if (!string.IsNullOrWhiteSpace(sortInput.Sorting))
                {
                    return query.OrderBy(sortInput.Sorting);
                }
            }

            // IQueryable.Take requires sorting, so we should sort if Take will be used.
            if (input is ILimitedResultRequest)
            {
                // This will throw exception if there is no Id column in entity class
                return query.OrderBy(defaultSortCriteria);
            }

            // No sorting
            return query;
        }

        protected virtual IQueryable<TEntity> ApplyPaging<TEntity, TInput>(IQueryable<TEntity> query, TInput input)
            where TEntity : class
        {
            // Try to use paging if available
            if (input is IPagedResultRequest pagedInput)
            {
                return query.PageBy(pagedInput);
            }

            // Try to limit query result if available
            if (input is ILimitedResultRequest limitedInput)
            {
                return query.Take(limitedInput.MaxResultCount);
            }

            // No paging
            return query;
        }
    }
}
