using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using Thunder.Platform.Core.Context;

namespace Thunder.Service.Authentication
{
    public class SimpleAuthorizationFilter : IActionFilter
    {
        private readonly IUserContext _userContext;

        public SimpleAuthorizationFilter(IUserContext userContext)
        {
            _userContext = userContext;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            bool hasUsername = !string.IsNullOrEmpty(_userContext.GetValue<string>(CommonUserContextKeys.Username));
            bool validThunderSecretKey = _userContext.GetValue<bool>(CommonUserContextKeys.ValidThunderSecretKey);

            if (hasUsername || validThunderSecretKey)
            {
                return;
            }

            throw new AuthenticationException();
        }
    }
}
