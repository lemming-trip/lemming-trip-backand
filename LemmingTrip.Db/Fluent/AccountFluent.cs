using LemmingTrip.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace LemmingTrip.Db.Fluent;

/// <summary>
/// Account table settings
/// </summary>
public static class AccountFluent
{
    /// <summary>
    /// Settings for Account table
    /// </summary>
    /// <param name="modelBuilder">Builder</param>
    /// <returns></returns>
    public static ModelBuilder Account(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(account =>
        {
            account.ToTable(t => { t.HasComment("Account of user"); });

            account
                .Property(p => p.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()")
                .IsRequired()
                .HasComment("Id of account");
            
            account
                .Property(e=>e.AccountProvider)
                .HasColumnType("varchar")
                .IsRequired()
                .HasComment("Account provider. Google, Facebook, etc.");

            account
                .Property(a => a.Password)
                .HasColumnType("varchar")
                .IsRequired()
                .HasComment("Password (encrypted)");

            account
                .Property(a => a.Salt)
                .HasColumnType("integer")
                .IsRequired()
                .HasComment("Salt (for password hashing)");

            account
                .Property(a => a.ActivationCode)
                .HasColumnType("uuid")
                .IsRequired()
                .HasComment("Activation code. Send by email and accepted by user");

            account
                .Property(a => a.IsVerified)
                .HasColumnName("is_verified")
                .HasColumnType("boolean")
                .IsRequired()
                .HasComment("Verified account");

            account
                .Property(a => a.LastLoginAt)
                .HasComment("Last login date");
        });

        return modelBuilder;
    }
}