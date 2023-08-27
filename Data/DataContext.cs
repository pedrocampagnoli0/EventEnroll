
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace EventEnroll.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>()
                    .HasMany(e => e.Attendees)
                    .WithMany(u => u.Events);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Creator)
                .WithMany(c => c.CreatedEvents)
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Event> Events => Set<Event>();
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

    }
}
