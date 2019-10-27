using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Thunder.Platform.Core;
using Thunder.Platform.Core.Context;
using Thunder.Platform.Core.Extensions;

namespace Thunder.Platform.AspNetCore.Context
{
    public class ThunderWebUserContextStore : IUserContextStore
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ThunderWebUserContextStore(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public T GetValue<T>(string contextKey)
        {
            Guard.NotNull(contextKey, nameof(contextKey));

            if (HttpContextIsNotAvailable())
            {
                return default;
            }

            string computedKey = UserContextKeyAttribute.ComputedContextKeyFor(contextKey);

            if (CurrentHttpContext().Items.ContainsKey(computedKey))
            {
                return (T)CurrentHttpContext().Items[computedKey];
            }

            return default;
        }

        public void SetValue(object value, string contextKey)
        {
            Guard.NotNull(contextKey, nameof(contextKey));

            if (HttpContextIsNotAvailable())
            {
                return;
            }

            string computedKey = UserContextKeyAttribute.ComputedContextKeyFor(contextKey);
            CurrentHttpContext().Items[computedKey] = value;
        }

        public List<string> GetAllKeys()
        {
            if (HttpContextIsNotAvailable())
            {
                return new List<string>();
            }

            return CurrentHttpContext().Items.Keys
                .Where(key => key is string keyString && keyString.StartsWith(UserContextKeyAttribute.ContextKeyPrefix))
                .Select(key => key.As<string>())
                .ToList();
        }

        public void Clear()
        {
            if (HttpContextIsNotAvailable())
            {
                return;
            }

            var keys = GetAllKeys();
            foreach (string key in keys)
            {
                CurrentHttpContext().Items.Remove(key);
            }
        }

        /// <summary>
        /// To check the availability of the HttContextAccessor.
        /// </summary>
        /// <returns>True if the accessor is not available and otherwise false.</returns>
        private bool HttpContextIsNotAvailable()
        {
            return _httpContextAccessor?.HttpContext == null;
        }

        /// <summary>
        /// To get the current http context.
        /// This method is very important and explain the reason why we don't store _httpContextAccessor.HttpContext
        /// to a private variable such as private HttpContext _context = _httpContextAccessor.HttpContext.
        /// The important reason is HttpContext property inside HttpContextAccessor is AsyncLocal property. That's why
        /// we need to keep this behavior or we will face the thread issue or accessing DisposedObject.
        /// More details at: https://github.com/aspnet/AspNetCore/blob/master/src/Http/Http/src/HttpContextAccessor.cs#L16.
        /// </summary>
        /// <returns>The current HttpContext with thread safe.</returns>
        private HttpContext CurrentHttpContext()
        {
            return _httpContextAccessor.HttpContext;
        }
    }
}
