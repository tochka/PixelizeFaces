using System.Data.Entity;
using PixelizeFaces.Domain.Entities;

namespace PixelizeFaces.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet<Picture> Picture { get; set; }
    }
}