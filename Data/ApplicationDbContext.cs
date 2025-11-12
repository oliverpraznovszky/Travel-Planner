using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using travel_planner.Models;

namespace travel_planner.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet-ek
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripParticipant> TripParticipants { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; }
        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User konfiguráció
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.PasswordHash).IsRequired();
            });

            // Trip konfiguráció
            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Budget).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.CreatedBy)
                    .WithMany(u => u.CreatedTrips)
                    .HasForeignKey(e => e.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // TripParticipant konfiguráció
            modelBuilder.Entity<TripParticipant>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Trip)
                    .WithMany(t => t.Participants)
                    .HasForeignKey(e => e.TripId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.TripParticipations)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Unique constraint: egy user csak egyszer lehet résztvevő egy trip-en
                entity.HasIndex(e => new { e.TripId, e.UserId }).IsUnique();
            });

            // Location konfiguráció
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);

                entity.HasOne(e => e.Trip)
                    .WithMany(t => t.Locations)
                    .HasForeignKey(e => e.TripId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Itinerary konfiguráció
            modelBuilder.Entity<Itinerary>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Trip)
                    .WithMany(t => t.Itineraries)
                    .HasForeignKey(e => e.TripId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Unique constraint: egy trip-en belül egy nap csak egyszer szerepelhet
                entity.HasIndex(e => new { e.TripId, e.DayNumber }).IsUnique();
            });

            // Activity konfiguráció
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.EstimatedCost).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Itinerary)
                    .WithMany(i => i.Activities)
                    .HasForeignKey(e => e.ItineraryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Location)
                    .WithMany(l => l.Activities)
                    .HasForeignKey(e => e.LocationId)
                    .OnDelete(DeleteBehavior.NoAction);  // ← Ez változott (SetNull → NoAction)
            });
        }
    }
}