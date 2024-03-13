using Microsoft.EntityFrameworkCore;
using NetGlade.Domain.Entities;

namespace NetGlade.Application.DatabaseContext
{
    public class NetGladeContext : DbContext
    {
        public NetGladeContext(DbContextOptions<NetGladeContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<EANCode> EANCodes => Set<EANCode>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Section> Sections => Set<Section>();
    }
}
