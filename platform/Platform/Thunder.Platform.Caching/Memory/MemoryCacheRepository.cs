using System;
using System.Runtime.Caching;
using Thunder.Platform.Core.Timing;

namespace Thunder.Platform.Caching.Memory
{
    public class MemoryCacheRepository : ICacheRepository, IDisposable
    {
        private readonly MemoryCache _cacheImpl;

        private readonly string _cacheName;

        public MemoryCacheRepository(string name)
        {
            _cacheName = name;
            _cacheImpl = new MemoryCache(GetName());
        }

        public T AddOrGetExisting<T>(ICacheKey key, Lazy<T> data)
        {
            if (!_cacheImpl.Contains(key.FullKey) && data != null)
            {
                _cacheImpl.Add(key.FullKey, data.Value, null);
            }

            return Get<T>(key);
        }

        public T AddOrGetExisting<T>(ICacheKey key, Lazy<T> data, TimeSpan expireIn)
        {
            var existingData = _cacheImpl[key.FullKey];
            if (existingData != null)
            {
                return (T)existingData;
            }

            Add(key, data.Value, expireIn);
            return Get<T>(key);
        }

        public bool Add<T>(ICacheKey key, T data, TimeSpan expireIn)
        {
            return _cacheImpl.Add(key.FullKey, data, new DateTimeOffset(Clock.Now + expireIn));
        }

        public T Get<T>(ICacheKey key)
        {
            return _cacheImpl[key.FullKey] != null ? (T)_cacheImpl[key.FullKey] : default(T);
        }

        public bool ContainsKey(ICacheKey key)
        {
            return _cacheImpl.Contains(key.FullKey);
        }

        public void Replace(ICacheKey key, object data)
        {
            if (data != null)
            {
                _cacheImpl[key.FullKey] = data;
            }
        }

        public void Clear()
        {
            _cacheImpl.Trim(100);
        }

        public string GetName()
        {
            return _cacheName;
        }

        public void Remove(ICacheKey key)
        {
            _cacheImpl.Remove(key.FullKey);
        }

        public void Dispose()
        {
            _cacheImpl.Dispose();
        }
    }
}
