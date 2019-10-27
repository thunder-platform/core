using System;
using System.Text.Json;
using Serilog;
using StackExchange.Redis;
using Thunder.Platform.Core.Json;

namespace Thunder.Platform.Caching.Redis
{
    public class RedisCacheRepository : ICacheRepository
    {
        private static readonly TimeSpan MaximumCacheDuration = TimeSpan.FromHours(4);

        private readonly string _cacheName;
        private readonly IRedisAccessorProvider _redisDbProvider;
        private readonly ILogger _logger;
        private readonly IDatabase _database;

        public RedisCacheRepository(string name, IRedisAccessorProvider redisDbProvider, ILogger logger)
        {
            _redisDbProvider = redisDbProvider;
            _logger = logger;
            _database = redisDbProvider.GetDatabase();
            _cacheName = name;
        }

        public bool Add<T>(ICacheKey key, T data, TimeSpan expireIn)
        {
            try
            {
                _database.StringSetAsync(key.FullKey, ConvertToJsonString(data), expireIn);
                _database.SetAddAsync(GetFullKeyForCachedItems(), key.FullKey);
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to set data in Redis cache.");
                return false;
            }
        }

        public T AddOrGetExisting<T>(ICacheKey key, Lazy<T> data)
        {
            return AddOrGetExisting(key, data, MaximumCacheDuration);
        }

        public T AddOrGetExisting<T>(ICacheKey key, Lazy<T> data, TimeSpan expireIn)
        {
            if (data == null)
            {
                throw new ArgumentException(@"The lazy fallback data may not be null", nameof(data));
            }

            if (expireIn > MaximumCacheDuration)
            {
                throw new ArgumentException($@"Maximum value for cache duration is {MaximumCacheDuration}.", nameof(expireIn));
            }

            if (TryGetValue(key, out T result))
            {
                return result;
            }

            // may (and should be able to) throw an exception
            var lazyValue = data.Value;

            Add(key, lazyValue, expireIn);

            return lazyValue;
        }

        public T Get<T>(ICacheKey key)
        {
            TryGetValue(key, out T result);
            return result;
        }

        public bool ContainsKey(ICacheKey key)
        {
            try
            {
                return _database.KeyExists(key.FullKey);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to check key existence from Redis-cache.");
                return false;
            }
        }

        public void Replace(ICacheKey key, object data)
        {
            Remove(key);
            if (data != null)
            {
                AddOrGetExisting(key, new Lazy<object>(() => data));
            }
        }

        public void Clear()
        {
            var fullKeyForCachedItems = GetFullKeyForCachedItems();

            if (!_database.KeyExists(fullKeyForCachedItems))
            {
                // if the key that stores the keys is not present, then the cache must be empty and
                // effectively flushed
                return;
            }

            var keys = _database.SetMembers(fullKeyForCachedItems);
            _database.SetRemoveAsync(fullKeyForCachedItems, keys);

            foreach (var k in keys)
            {
                _database.KeyDeleteAsync(k.ToString());
            }
        }

        public string GetName()
        {
            return _cacheName;
        }

        public void Remove(ICacheKey key)
        {
            _database.SetRemoveAsync(GetFullKeyForCachedItems(), key.FullKey);
            _database.KeyDeleteAsync(key.FullKey);
        }

        private bool TryGetValue<T>(ICacheKey key, out T result)
        {
            try
            {
                if (ContainsKey(key))
                {
                    result = GetReturnedObject<T>(_database.StringGet(key.FullKey));
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to get data from Redis-cache.");
            }

            result = default(T);
            return false;
        }

        private string ConvertToJsonString<T>(T data)
        {
            return data == null ? null : JsonSerializer.Serialize(data, ThunderJsonSerializerOptions.SharedJsonSerializerOptions);
        }

        private T GetReturnedObject<T>(string jsonData)
        {
            return jsonData == null ? default(T) : JsonSerializer.Deserialize<T>(jsonData, ThunderJsonSerializerOptions.SharedJsonSerializerOptions);
        }

        private string GetFullKeyForCachedItems()
        {
            return new CacheKey(_cacheName, "CachedItemsInRepository", true).FullKey;
        }
    }
}
