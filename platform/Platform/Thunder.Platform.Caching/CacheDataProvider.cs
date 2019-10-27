using System;
using System.Collections.Generic;
using System.Linq;

namespace Thunder.Platform.Caching
{
    public class CacheDataProvider : ICacheDataProvider
    {
        private readonly IEnumerable<ICacheRepository> _cachedRepositories;

        public CacheDataProvider(IEnumerable<ICacheRepository> cachedRepositories)
        {
            _cachedRepositories = cachedRepositories ?? throw new ArgumentNullException(nameof(cachedRepositories));
        }

        public T GetCachedData<T>(ICacheKey key, Lazy<T> dataProvider)
        {
            var cacheRepository = FindRepository(key.RepositoryName);
            return cacheRepository.AddOrGetExisting(key, dataProvider);
        }

        public T GetCachedData<T>(ICacheKey key, Lazy<T> dataProvider, TimeSpan expireIn)
        {
            var cacheRepository = FindRepository(key.RepositoryName);
            return cacheRepository.AddOrGetExisting(key, dataProvider, expireIn);
        }

        public void RemoveCacheData(ICacheKey key)
        {
            var cacheRepository = FindRepository(key.RepositoryName);
            cacheRepository.Remove(key);
        }

        public T GetCachedData<T>(ICacheKey key)
        {
            var cacheRepository = FindRepository(key.RepositoryName);
            return cacheRepository.Get<T>(key);
        }

        public bool AddCachedData<T>(ICacheKey key, T data, TimeSpan expireIn)
        {
            var cacheRepository = FindRepository(key.RepositoryName);
            return cacheRepository.Add(key, data, expireIn);
        }

        public bool ContainsKey(ICacheKey key)
        {
            var cacheRepository = FindRepository(key.RepositoryName);
            return cacheRepository.ContainsKey(key);
        }

        public void ClearRepository(string repositoryName)
        {
            var cacheRepository = FindRepository(repositoryName);
            cacheRepository.Clear();
        }

        public void ClearAll()
        {
            foreach (var cachedRepository in _cachedRepositories)
            {
                cachedRepository.Clear();
            }
        }

        private ICacheRepository FindRepository(string repositoryName)
        {
            var cacheRepository = _cachedRepositories.SingleOrDefault(c => StringComparer.OrdinalIgnoreCase.Equals(repositoryName, c.GetName()));
            if (cacheRepository == null)
            {
                throw new ArgumentOutOfRangeException($"Cache '{repositoryName}' could not be found.");
            }

            return cacheRepository;
        }
    }
}
