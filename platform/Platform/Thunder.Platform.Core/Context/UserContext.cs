using System;
using System.Collections.Generic;

namespace Thunder.Platform.Core.Context
{
    public class UserContext : IUserContext
    {
        [Experimental("This store should be provided at host project, otherwise it will user thread static variable.")]
        private static IUserContextStore _userContextStore = new ThreadUserContextStore();

        private UserContext()
        {
        }

        [Experimental("This instance is used only for framework. You should inject IUserContext instead using this instance directly.")]
        public static IUserContext Current { get; } = new UserContext();

        public T GetValue<T>(string contextKey)
        {
            Guard.NotNull(contextKey, nameof(contextKey));

            return _userContextStore.GetValue<T>(contextKey);
        }

        public void SetValue(object value, string contextKey)
        {
            Guard.NotNull(contextKey, nameof(contextKey));

            _userContextStore.SetValue(value, contextKey);
        }

        public void SetUserContextStore(IUserContextStore userContextStore)
        {
            _userContextStore = userContextStore ?? throw new ArgumentNullException(nameof(userContextStore));
        }

        public UserContextContents ExportContents()
        {
            var nameToValue = new Dictionary<string, object>();
            var keys = _userContextStore.GetAllKeys();
            foreach (string key in keys)
            {
                nameToValue.Add(key, _userContextStore.GetValue<object>(key));
            }

            return new UserContextContents(nameToValue);
        }

        public void ImportContents(UserContextContents userContextContents)
        {
            // if we import a different context, ensure, that we don't keep stuff from the outer scope.
            Clear();

            foreach (var nameToValue in userContextContents.NameToValueMap)
            {
                _userContextStore.SetValue(nameToValue.Value, nameToValue.Key);
            }
        }

        public void Clear()
        {
            _userContextStore.Clear();
        }
    }
}
