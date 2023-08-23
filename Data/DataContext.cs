
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EventEnroll.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Event> Events => Set<Event>();

    }
}
