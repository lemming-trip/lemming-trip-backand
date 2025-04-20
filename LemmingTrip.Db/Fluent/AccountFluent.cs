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
                .HasOne(p => p.User)
                .WithOne(p => p.Account)
                .HasForeignKey<User>(p => p.UserId);

            account
                .Property(p => p.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()")
                .IsRequired()
                .HasComment("Id of account");

            account
                .Property(a => a.Email)
                .HasColumnType("varchar")
                .IsRequired()
                .HasComment("Post address");

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
                .Property(a => a.IsActive)
                .HasColumnType("bool")
                .IsRequired()
                .HasComment("Is account active or not");

            account
                .Property(a => a.ActivationCode)
                .HasColumnType("uuid")
                .IsRequired()
                .HasComment("Activation code. Send by email and accepted by user");

            account
                .Property(a => a.RegistrationDate)
                .HasColumnName("registration_date")
                .IsRequired()
                .HasComment("Registration date of user account");

            account
                .Property(a => a.AccountRole)
                .HasConversion<string>()
                .HasColumnType("varchar")
                .IsRequired()
                .HasComment("Post address");
        });

        return modelBuilder;
    }
}