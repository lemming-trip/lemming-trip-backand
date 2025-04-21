using LemmingTrip.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace LemmingTrip.Db.Fluent;

/// <summary>
/// User table settings
/// </summary>
public static class UserFluent
{
    /// <summary>
    /// Settings for User table
    /// </summary>
    /// <param name="modelBuilder">Builder</param>
    /// <returns></returns>
    public static ModelBuilder User(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.ToTable(t => { t.HasComment("User"); });

            user
                .Property(e => e.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()")
                .IsRequired()
                .HasComment("Id of user");
            
            user
                .Property(e => e.Avatar)
                .HasColumnName("avatar")
                .HasColumnType("varchar")
                .HasMaxLength(2000)
                .HasComment("Avatar (Image)");
            
            user
                .Property(e => e.Phone)
                .HasColumnName("phone")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasComment("Phone");
            
            user
                .Property(e => e.Address)
                .HasColumnName("address")
                .HasColumnType("varchar")
                .HasMaxLength(2000)
                .HasComment("Address");
            
            user
                .Property(e => e.FirstName)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasComment("First name");
            
            user
                .Property(e => e.LastName)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasComment("Last name");
            
            user
                .Property(e => e.MiddleName)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasComment("Middle name");
            
            user
                .Property(e => e.DateBirth)
                .HasComment("Birth date");
            
            user
                .Property(e => e.Description)
                .HasColumnName("description")
                .HasColumnType("text")
                .HasComment("Description");
            
            user.HasMany(e=>e.Accounts)
                .WithOne(e=>e.User)
                .HasForeignKey(e=>e.UserId)
                .HasConstraintName("Relationship between User and Account tables. One to many.");

        });

        return modelBuilder;
    }
}