using LemmingTrip.Db.Entities.Base;
using NetTopologySuite.Geometries;

namespace LemmingTrip.Db.Entities;

/// <summary>
/// Путешествие
/// </summary>
public class Trip
{
    /// <summary>
    /// Идентификатор путешествия
    /// </summary>
    public Guid TripId { get; set; }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Заголовок
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Описание
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Титульное изображение
    /// </summary>
    public string? TitleImage { get; set; }

    /// <summary>
    /// Дополнительные изображения
    /// </summary>
    public IList<string>? Images { get; set; }

    /// <summary>
    /// Ссылка на видео
    /// </summary>
    public string? VideoLink { get; set; }

    /// <summary>
    /// Координаты маршрута
    /// </summary>
    public Geometry? Route { get; set; }

    /// <summary>
    /// Рейтинг
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Лайки
    /// </summary>
    public int Likes { get; set; }

    /// <summary>
    /// Тип тура
    /// </summary>
    public TripType TripType { get; set; }

    /// <summary>
    /// Тип поиска для путешествия. Кого ищу.
    /// </summary>
    public TripSearchType TripSearchType { get; set; }
    
    /// <summary>
    /// Связь с таблицей User
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Связь с таблицей TripReply
    /// </summary>
    public IList<TripReply>? TripReplies { get; set; }
}