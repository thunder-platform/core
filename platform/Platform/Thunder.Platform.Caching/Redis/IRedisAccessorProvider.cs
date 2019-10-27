using StackExchange.Redis;

namespace Thunder.Platform.Caching.Redis
{
    public interface IRedisAccessorProvider
    {
        IDatabase GetDatabase();

        IConnectionMultiplexer GetConnection();

        void CloseConnection();
    }
}
