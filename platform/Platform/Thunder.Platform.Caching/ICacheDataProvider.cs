using System;

namespace Thunder.Platform.Caching
{
    public interface ICacheDataProvider
    {
        T GetCachedData<T>(ICacheKey key, Lazy<T> dataProvider);

        T GetCachedData<T>(ICacheKey key, Lazy<T> dataProvider, TimeSpan expireIn);

        T GetCachedData<T>(ICacheKey key);

        bool AddCachedData<T>(ICacheKey key, T data, TimeSpan expireIn);

        void RemoveCacheData(ICacheKey key);

        void ClearRepository(string repositoryName);

        void ClearAll();

        bool ContainsKey(ICacheKey key);
    }
}
