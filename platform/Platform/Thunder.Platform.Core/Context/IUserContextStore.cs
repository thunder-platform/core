using System.Collections.Generic;

namespace Thunder.Platform.Core.Context
{
    public interface IUserContextStore
    {
        T GetValue<T>(string contextKey = "");

        void SetValue(object value, string contextKey = "");

        List<string> GetAllKeys();

        void Clear();
    }
}
