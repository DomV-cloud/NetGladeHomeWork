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
    }
}
