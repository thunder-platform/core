using System;
using System.Collections.Generic;
using System.Linq;

namespace Thunder.Platform.Core.Context
{
    public class ThreadUserContextStore : IUserContextStore
    {
        [ThreadStatic]
        private static Dictionary<string, object> _userContextData;

        private static Dictionary<string, object> ThreadUserContextData => _userContextData ?? (_userContextData = new Dictionary<string, object>());

        public T GetValue<T>(string contextKey)
        {
            Guard.NotNull(contextKey, nameof(contextKey));

            string computedKey = UserContextKeyAttribute.ComputedContextKeyFor(contextKey);

            if (ThreadUserContextData.ContainsKey(computedKey))
            {
                return (T)ThreadUserContextData[computedKey];
            }

            return default;
        }

        public void SetValue(object value, string contextKey)
        {
            Guard.NotNull(contextKey, nameof(contextKey));

            string computedKey = UserContextKeyAttribute.ComputedContextKeyFor(contextKey);
            ThreadUserContextData[computedKey] = value;
        }

        public List<string> GetAllKeys()
        {
            return ThreadUserContextData.Keys.ToList();
        }

        public void Clear()
        {
            var keys = GetAllKeys();
            foreach (string key in keys)
            {
                SetValue(null, key);
            }
        }
    }
}
