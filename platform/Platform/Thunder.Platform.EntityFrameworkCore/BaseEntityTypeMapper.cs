using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thunder.Platform.EntityFrameworkCore
{
    public abstract class BaseEntityTypeMapper<TEntityType> : IEntityTypeMapper
        where TEntityType : class
    {
        public void Map(ModelBuilder builder)
        {
            InternalMap(builder.Entity<TEntityType>());
        }

        protected abstract void InternalMap(EntityTypeBuilder<TEntityType> builder);
    }
}
