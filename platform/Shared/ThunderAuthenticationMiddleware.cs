using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.Core.Context;

namespace Thunder.Service.Authentication
{
    public class ThunderAuthenticationMiddleware
    {
        private const string ThunderSecretKeyName = "ThunderSecretKey";
        private readonly string _secretKey;
        private readonly RequestDelegate _next;

        public ThunderAuthenticationMiddleware(
            RequestDelegate next,
            IAuthenticationSchemeProvider schemes,
            IConfiguration configuration)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (schemes == null)
            {
                throw new ArgumentNullException(nameof(schemes));
            }

            _secretKey = configuration.GetValue<string>(ThunderSecretKeyName);
            _next = next;
            Schemes = schemes;
        }

        public IAuthenticationSchemeProvider Schemes { get; set; }

        public async Task Invoke(HttpContext context, IUserContext userContext)
        {
            context.Features.Set<IAuthenticationFeature>(new AuthenticationFeature
            {
                OriginalPath = context.Request.Path,
                OriginalPathBase = context.Request.PathBase
            });

            // Give any IAuthenticationRequestHandler schemes a chance to handle the request
            var handlers = context.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
            foreach (var scheme in await Schemes.GetRequestHandlerSchemesAsync())
            {
                if (await handlers.GetHandlerAsync(context, scheme.Name) is IAuthenticationRequestHandler handler
                    && await handler.HandleRequestAsync())
                {
                    return;
                }
            }

            // This case is for Azure AD authentication with JWT.
            var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate != null)
            {
                var result = await context.AuthenticateAsync(defaultAuthenticate.Name);
                if (result.Succeeded)
                {
                    var principal = result.Principal;
                    var name = principal.FindFirst("name").Value;
                    context.User = principal;
                    userContext.SetValue(name, CommonUserContextKeys.Username);
                }
            }

            // This case is for ThunderSecretKey.
            string secretKey = context.Request.Headers[ThunderSecretKeyName];
            if (!string.IsNullOrEmpty(secretKey) && _secretKey.Equals(secretKey))
            {
                userContext.SetValue(true, CommonUserContextKeys.ValidThunderSecretKey);
            }

            await _next(context);
        }
    }
}
