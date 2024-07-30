using LemmingTrip.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace LemmingTrip.Db.Fluent;

/// <summary>
/// Настройка таблицы Trip
/// </summary>
public static class TripFluent
{
    /// <summary>
    /// Настройки
    /// </summary>
    /// <param name="modelBuilder">Билдер</param>
    /// <returns></returns>
    public static ModelBuilder Trip(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trip>(trip =>
        {
            trip.ToTable(t => t.HasComment("Путешествие"));

            trip
                .Property(p => p.TripId)
                .HasColumnName("trip_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()")
                .IsRequired()
                .HasComment("Идентификатор");

            trip
                .Property(p => p.Title)
                .IsRequired()
                .HasComment("Заголовок. Название путешествия.");

            trip
                .Property(p => p.Text)
                .HasComment("Описание");

            trip
                .Property(p => p.Images)
                .HasComment("Изображения");

            trip
                .Property(p => p.VideoLink)
                .HasComment("Ссылка на видео");

            trip
                .Property(p => p.Route)
                .HasComment("Маршрут на карте");

            trip.Property(p => p.Rating)
                .IsRequired()
                .HasComment("Рейтинг путешествия");

            trip.Property(p => p.Likes)
                .IsRequired()
                .HasComment("Лайки");

            trip
                .Property(p => p.TripType)
                .HasConversion<string>()
                .HasColumnType("varchar")
                .IsRequired()
                .HasComment("Тип путешествия");

            trip
                .Property(p => p.TripSearchType)
                .HasConversion<string>()
                .HasColumnType("varchar")
                .IsRequired()
                .HasComment("Тип поиска для путешествия");

            trip.HasOne(p => p.User)
                .WithMany(p => p.Trips)
                .HasForeignKey(p => p.UserId)
                .IsRequired()
                .HasConstraintName("Связь между таблицами User и Trip. Один ко многим.");
            
            trip
                .HasMany(p=> p.TripReplies)
                .WithOne(p=>p.Trip)
                .HasForeignKey(p=>p.TripId)
                .HasConstraintName("Связь между таблицами Trip и TripReplies. Один ко многим.");
        });

        return modelBuilder;
    }
}