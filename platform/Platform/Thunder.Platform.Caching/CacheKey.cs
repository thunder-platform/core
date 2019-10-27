using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Thunder.Platform.Core.Context;

namespace Thunder.Platform.Caching
{
    /// <summary>
    /// This class represents the key of cached data.
    /// </summary>
    [DebuggerDisplay("FullKey={" + nameof(FullKey) + "}")]
    public class CacheKey : ICacheKey
    {
        public CacheKey(string repositoryName, string key, bool isGlobal) : this(repositoryName, new[] { key }, isGlobal)
        {
        }

        public CacheKey(string repositoryName, string[] keys, bool isGlobal)
        {
            if (string.IsNullOrEmpty(repositoryName))
            {
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(repositoryName));
            }

            if (keys == null || keys.Length == 0)
            {
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(keys));
            }

            RepositoryName = repositoryName;
            FullKey = BuildKey(keys, isGlobal);
        }

        protected CacheKey(IList<string> keys)
        {
            if (keys == null || !keys.Any())
            {
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(keys));
            }

            RepositoryName = GetType().Name;
            FullKey = string.Join("_", new[] { RepositoryName }.Concat(keys.Where(k => k != null)));
        }

        protected CacheKey(string key) : this(new[] { key })
        {
        }

        public string RepositoryName { get; }

        public string FullKey { get; }

        private string BuildKey(string[] keys, bool isGlobal)
        {
            var keysToBuild = new List<string> { RepositoryName };
            keysToBuild.AddRange(keys);
            if (!isGlobal)
            {
                keysToBuild.Add(UserContext.Current.GetValue<string>(CommonUserContextKeys.TenantId));
            }

            var combinedKey = string.Join("_", keysToBuild);
            return combinedKey;
        }
    }
}
