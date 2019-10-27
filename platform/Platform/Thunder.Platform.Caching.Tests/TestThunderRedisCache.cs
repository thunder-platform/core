using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog.Core;
using Thunder.Platform.Caching.Redis;
using Thunder.Platform.Testing;
using Xunit;

namespace Thunder.Platform.Caching.Tests
{
    public class TestThunderRedisCache : BaseThunderTest
    {
        [Fact]
        public void Connect_to_redis_should_be_ok()
        {
            var nullLogger = Logger.None;
            var options = Provider.GetService<IOptions<ThunderRedisCacheOptions>>();

            var cacheProvider = new CacheDataProvider(new ICacheRepository[]
            {
                new RedisCacheRepository(
                    "AppSettings",
                    new RedisAccessorProvider(options, nullLogger),
                    nullLogger)
            });

            var cacheKey = new CacheKey("AppSettings", "Hello", false);
            cacheProvider.AddCachedData(cacheKey, true, TimeSpan.MaxValue);

            var cacheValue = cacheProvider.GetCachedData<bool>(cacheKey);

            Assert.True(cacheValue);
        }

        protected override IEnumerable<KeyValuePair<string, string>> SetupInMemoryConfiguration()
        {
            return new[]
            {
                new KeyValuePair<string, string>(
                    "ThunderRedisCacheOptions:RedisConnectionString",
                    "thunder.redis.cache.windows.net:6380,password=qeyXkvM3gUGBme177Ii5Y8HRzBVPV9b2ltTsVzBwvHQ=,ssl=True,abortConnect=False")
            };
        }

        protected override IServiceCollection SetupAppServices(IServiceCollection services)
        {
            services.Configure<ThunderRedisCacheOptions>(Configuration.GetSection("ThunderRedisCacheOptions"));
            return base.SetupAppServices(services);
        }
    }
}
