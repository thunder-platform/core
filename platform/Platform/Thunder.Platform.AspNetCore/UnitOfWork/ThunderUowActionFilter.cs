using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Thunder.Platform.AspNetCore.Extensions;
using Thunder.Platform.Core.Domain.UnitOfWork;
using Thunder.Platform.Core.Domain.UnitOfWork.Abstractions;

namespace Thunder.Platform.AspNetCore.UnitOfWork
{
    public class ThunderUowActionFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IUnitOfWorkDefaultOptions _unitOfWorkDefaultOptions;

        public ThunderUowActionFilter(
            IUnitOfWorkManager unitOfWorkManager,
            IUnitOfWorkDefaultOptions unitOfWorkDefaultOptions)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _unitOfWorkDefaultOptions = unitOfWorkDefaultOptions;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                await next();
                return;
            }

            var unitOfWorkAttr = _unitOfWorkDefaultOptions.GetUnitOfWorkAttributeOrNull(context.ActionDescriptor.GetMethodInfo()) ??
                                 new UnitOfWorkAttribute();

            if (unitOfWorkAttr.IsDisabled)
            {
                await next();
                return;
            }

            using (var uow = _unitOfWorkManager.Begin(unitOfWorkAttr.CreateOptions()))
            {
                var result = await next();
                if (result.Exception == null || result.ExceptionHandled)
                {
                    await uow.CompleteAsync();
                }
            }
        }
    }
}
