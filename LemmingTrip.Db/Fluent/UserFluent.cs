using LemmingTrip.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace LemmingTrip.Db.Fluent;

/// <summary>
/// Настройки таблицы User
/// </summary>
public static class UserFluent
{
    /// <summary>
    /// Настройки
    /// </summary>
    /// <param name="modelBuilder">Билдер</param>
    /// <returns></returns>
    public static ModelBuilder User(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.ToTable(t => { t.HasComment("Пользователь"); });

            user
                .Property(p => p.UserId)
                .HasColumnName("user_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()")
                .IsRequired()
                .HasComment("Идентификатор");
            
            user
                .Property(a => a.Avatar)
                .HasColumnName("avatar")
                .HasColumnType("varchar")
                .HasComment("Аватар (картинка)");
            
            user
                .Property(a => a.Phone)
                .HasColumnName("phone")
                .HasColumnType("varchar")
                .HasComment("Телефон");
            
            user
                .Property(a => a.Address)
                .HasColumnName("address")
                .HasColumnType("varchar")
                .HasComment("Адрес");
            
            user
                .Property(p => p.FirstName)
                .HasColumnType("varchar")
                .HasComment("Фамилия");
            
            user
                .Property(p => p.LastName)
                .HasColumnType("varchar")
                .HasComment("Имя");
            
            user
                .Property(p => p.MiddleName)
                .HasColumnType("varchar")
                .HasComment("Отчество");
            
            user
                .Property(p => p.DateBirth)
                .HasComment("Дата рождения");
            
            user
                .Property(a => a.Description)
                .HasColumnName("description")
                .HasColumnType("text")
                .HasComment("Описание");

        });

        return modelBuilder;
    }
}