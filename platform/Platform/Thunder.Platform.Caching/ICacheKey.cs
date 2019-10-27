namespace Thunder.Platform.Caching
{
    public interface ICacheKey
    {
        /// <summary>
        /// The name of the repository in which the cached data will live.
        /// </summary>
        string RepositoryName { get; }

        /// <summary>
        /// The Full key of cached data. i.e. the data are saved under this name in cache.
        /// </summary>
        string FullKey { get; }
    }
}
