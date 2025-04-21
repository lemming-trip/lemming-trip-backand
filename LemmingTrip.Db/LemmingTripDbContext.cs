using Microsoft.EntityFrameworkCore;
using LemmingTrip.Db.Entities;
using LemmingTrip.Db.Fluent;

namespace LemmingTrip.Db;

/// <summary>
/// Database context
/// </summary>
public class LemmingTripDbContext(DbContextOptions<LemmingTripDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Account table
    /// </summary>
    public DbSet<Account> Accounts { get; set; } = null!;
    /// <summary>
    /// User table
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        modelBuilder.User();
        modelBuilder.Account();
    }
    
    /// <summary>
    /// Trip table
    /// </summary>
    // public DbSet<Trip> Trips { get; set; } = null!;
    /// <summary>
    /// TripReply table
    /// </summary>
    // public DbSet<TripReply> TripReplies { get; set; } = null!;
    /// <summary>
    /// TripReplyMessage table
    /// </summary>
    // public DbSet<TripReplyMessage> TripReplyMessages { get; set; } = null!;
}