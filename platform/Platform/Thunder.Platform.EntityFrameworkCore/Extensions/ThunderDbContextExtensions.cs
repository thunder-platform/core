using Microsoft.EntityFrameworkCore;

namespace Thunder.Platform.EntityFrameworkCore.Extensions
{
    public static class ThunderDbContextExtensions
    {
        public static DbSet<TEntityType> DbSet<TEntityType>(this BaseThunderDbContext context)
            where TEntityType : class
        {
            return context.Set<TEntityType>();
        }
    }
}
