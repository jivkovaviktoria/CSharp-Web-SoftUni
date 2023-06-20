using Homies.Data.Contracts;
using Homies.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Type = Homies.Data.Entities.Type;

namespace Homies.Data
{
    public class HomiesDbContext : IdentityDbContext<ApplicationUser>
    {
        public HomiesDbContext(DbContextOptions<HomiesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Type> Types { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Type>()
                .HasData(new Type()
                {
                    Id = 1,
                    Name = "Animals"
                },
                new Type()
                {
                    Id = 2,
                    Name = "Fun"
                },
                new Type()
                {
                    Id = 3,
                    Name = "Discussion"
                },
                new Type()
                {
                    Id = 4,
                    Name = "Work"
                });

            this.GetDefaultConfiguration<Event>(modelBuilder);
            this.GetDefaultConfiguration<Type>(modelBuilder);
            modelBuilder.Entity<EventParticipant>().HasKey(x => new { x.HelperId, x.EventId });

            modelBuilder.Entity<Event>()
                .HasOne(x => x.Organiser)
                .WithMany(x => x.Events)
                .HasForeignKey(x => x.OrganiserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
                .HasOne(x => x.Type)
                .WithMany(x => x.Events)
                .HasForeignKey(x => x.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventParticipant>()
                .HasOne(x => x.Helper)
                .WithMany(x => x.EventParticipants)
                .HasForeignKey(x => x.HelperId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventParticipant>()
                .HasOne(x => x.Event)
                .WithMany(x => x.EventsParticipants)
                .HasForeignKey(x => x.EventId)
                .OnDelete(DeleteBehavior.Restrict);
            
            base.OnModelCreating(modelBuilder);
        }

        private void GetDefaultConfiguration<TEntity>(ModelBuilder builder) where TEntity : class, IEntity
                => builder.Entity<TEntity>().HasKey(x => x.Id);
    }
}