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
    public string? Description { get; set; }

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
    public LineString? Route { get; set; }

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

/// <summary>
/// Отклик на путешествие
/// </summary>
public class TripReply
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid TripReplyId { get; set; }

    /// <summary>
    /// Идентификатор путешествия
    /// </summary>
    public Guid TripId { get; set; }

    /// <summary>
    /// Идентификатор пользователя, который откликнулся
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Текст отклика
    /// </summary>
    public string Text { get; set; } = null!;

    /// <summary>
    /// Дополнительные изображения
    /// </summary>
    public IList<string>? Images { get; set; }

    /// <summary>
    /// Ссылка на видео
    /// </summary>
    public string? VideoLink { get; set; }
    
    /// <summary>
    /// Ссылки на сообщения
    /// </summary>
    public IList<TripReplyMessage>? TripReplyMessages { get; set; }

    /// <summary>
    /// Связь с таблицей Trip
    /// </summary>
    public Trip Trip { get; set; } = null!;
    
    /// <summary>
    /// Связь с таблицей User
    /// </summary>
    public User User { get; set; } = null!;
}

/// <summary>
/// Сообщения для откликов на путешествие
/// </summary>
public class TripReplyMessage
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid TripReplyMessageId { get; set; }

    /// <summary>
    /// Идентификатор отклика на путешествие
    /// </summary>
    public Guid TripReplyId { get; set; }

    /// <summary>
    /// Идентификатор пользователя, который отправил сообщение
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string Text { get; set; } = null!;

    /// <summary>
    /// Изображения
    /// </summary>
    public IList<string>? Images { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedDateTime { get; set; }

    /// <summary>
    /// Статус сообщения. Прочитано или нет
    /// </summary>
    public bool IsRead { get; set; }

    /// <summary>
    /// Связь с таблицей User
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Связь с таблицей TripReply
    /// </summary>
    public TripReply TripReply { get; set; } = null!;
}