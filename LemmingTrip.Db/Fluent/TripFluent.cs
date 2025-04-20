using LemmingTrip.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace LemmingTrip.Db.Fluent;

/// <summary>
/// Trip table settings
/// </summary>
public static class TripFluent
{
    /// <summary>
    /// Settings for Trip table
    /// </summary>
    /// <param name="modelBuilder">Builder</param>
    /// <returns></returns>
    public static ModelBuilder Trip(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trip>(trip =>
        {
            trip.ToTable(t => t.HasComment("Trip"));

            trip
                .Property(p => p.Id)
                .HasColumnName("trip_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()")
                .IsRequired()
                .HasComment("Identifier");

            trip
                .Property(p => p.Title)
                .IsRequired()
                .HasComment("Title. Name of the trip");

            trip
                .Property(p => p.Text)
                .HasComment("Description of the trip");

            trip
                .Property(p => p.Images)
                .HasComment("Trip images");

            trip
                .Property(p => p.VideoLink)
                .HasComment("Video link");

            trip
                .Property(p => p.Route)
                .HasComment("Route on a map");

            trip.Property(p => p.Rating)
                .IsRequired()
                .HasComment("Trip rating");

            trip.Property(p => p.Likes)
                .IsRequired()
                .HasComment("Likes count");

            trip
                .Property(p => p.TripType)
                .HasConversion<string>()
                .HasColumnType("varchar")
                .IsRequired()
                .HasComment("Type of trip");

            trip
                .Property(p => p.TripSearchType)
                .HasConversion<string>()
                .HasColumnType("varchar")
                .IsRequired()
                .HasComment("Type search of trip");

            trip.HasOne(p => p.User)
                .WithMany(p => p.Trips)
                .HasForeignKey(p => p.UserId)
                .IsRequired()
                .HasConstraintName("Relationship between Trip and User tables. One to many.");
            
            trip
                .HasMany(p=> p.TripReplies)
                .WithOne(p=>p.Trip)
                .HasForeignKey(p=>p.TripId)
                .HasConstraintName("Relationship between Trip and TripReply tables. One to many.");
        });

        return modelBuilder;
    }
}