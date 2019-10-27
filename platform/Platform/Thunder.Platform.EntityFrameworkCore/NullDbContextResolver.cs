using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Thunder.Platform.EntityFrameworkCore
{
    public class NullDbContextResolver : IDbContextResolver
    {
        public TDbContext Resolve<TDbContext>(string connectionString) where TDbContext : BaseThunderDbContext
        {
            throw new NotSupportedException("Please replace the null service with your service. Example: services.Replace(ServiceDescriptor.Singleton<IDbContextResolver, YourContextResolver>());");
        }

        public TDbContext Resolve<TDbContext>(string connectionString, DbConnection connection) where TDbContext : BaseThunderDbContext
        {
            throw new NotSupportedException("Please replace the null service with your service. Example: services.Replace(ServiceDescriptor.Singleton<IDbContextResolver, YourContextResolver>());");
        }
    }
}
