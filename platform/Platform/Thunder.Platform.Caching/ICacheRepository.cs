using System;

namespace Thunder.Platform.Caching
{
    public interface ICacheRepository
    {
        string GetName();

        T AddOrGetExisting<T>(ICacheKey key, Lazy<T> data);

        T AddOrGetExisting<T>(ICacheKey key, Lazy<T> data, TimeSpan expireIn);

        bool Add<T>(ICacheKey key, T data, TimeSpan expireIn);

        T Get<T>(ICacheKey key);

        bool ContainsKey(ICacheKey key);

        void Replace(ICacheKey key, object data);

        void Clear();

        void Remove(ICacheKey key);
    }
}
