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
                .Property(p => p.UserId)
                .HasColumnName("user_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()")
                .IsRequired()
                .HasComment("Id of user");
            
            user
                .Property(a => a.Avatar)
                .HasColumnName("avatar")
                .HasColumnType("varchar")
                .HasComment("Avatar (Image)");
            
            user
                .Property(a => a.Phone)
                .HasColumnName("phone")
                .HasColumnType("varchar")
                .HasComment("Phone");
            
            user
                .Property(a => a.Address)
                .HasColumnName("address")
                .HasColumnType("varchar")
                .HasComment("Address");
            
            user
                .Property(p => p.FirstName)
                .HasColumnType("varchar")
                .HasComment("First name");
            
            user
                .Property(p => p.LastName)
                .HasColumnType("varchar")
                .HasComment("Last name");
            
            user
                .Property(p => p.MiddleName)
                .HasColumnType("varchar")
                .HasComment("Middle name");
            
            user
                .Property(p => p.DateBirth)
                .HasComment("Birth date");
            
            user
                .Property(a => a.Description)
                .HasColumnName("description")
                .HasColumnType("text")
                .HasComment("Description");

        });

        return modelBuilder;
    }
}