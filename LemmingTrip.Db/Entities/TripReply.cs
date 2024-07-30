namespace LemmingTrip.Db.Entities;

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