using System;
using System.Collections.Generic;
using System.IO;
using Thunder.Platform.Testing;

namespace Thunder.Platform.EntityFrameworkCore.Tests
{
    public abstract class BaseEntityFrameworkCoreTest : BaseThunderTest
    {
        protected override IEnumerable<KeyValuePair<string, string>> SetupInMemoryConfiguration()
        {
            return new[]
            {
                new KeyValuePair<string, string>(
                    "ConnectionStrings:Default",
                    $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestDb")}.db")
            };
        }

        protected string ConnectionStringFor(string dbName)
        {
            return $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbName)}.db";
        }
    }
}
