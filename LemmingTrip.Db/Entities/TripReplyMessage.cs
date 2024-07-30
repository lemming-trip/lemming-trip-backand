namespace LemmingTrip.Db.Entities;

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