using Microsoft.EntityFrameworkCore;

namespace Thunder.Platform.EntityFrameworkCore
{
    public interface IEntityTypeMapper
    {
        void Map(ModelBuilder modelBuilder);
    }
}
