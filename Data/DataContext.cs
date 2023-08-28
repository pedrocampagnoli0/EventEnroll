
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
            //makes attendee table without primary key

            //modelBuilder.Entity<Event>()
            //        .HasMany(e => e.Attendees)
            //        .WithMany(u => u.Events);

            modelBuilder.Entity<Event>()
            .HasMany(e => e.Attendees) // Event has many attendees (users)
            .WithMany() // Users can attend many events
            .UsingEntity(j => j.ToTable("EventAttendees")); // Configure the junction table name

        }
        public DbSet<Event> Events => Set<Event>();

    }
}
