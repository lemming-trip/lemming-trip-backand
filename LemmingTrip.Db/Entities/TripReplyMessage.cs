namespace LemmingTrip.Db.Entities;

/// <summary>
/// Message in reply to trip
/// </summary>
public class TripReplyMessage
{
    /// <summary>
    /// Trip reply message id
    /// </summary>
    public Guid TripReplyMessageId { get; set; }

    /// <summary>
    /// Identifier of the reply to the trip
    /// </summary>
    public Guid TripReplyId { get; set; }

    /// <summary>
    /// User id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Message text
    /// </summary>
    public string Text { get; set; } = null!;

    /// <summary>
    /// Images
    /// </summary>
    public IList<string>? Images { get; set; }

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreatedDateTime { get; set; }

    /// <summary>
    /// Message status. Read or unread
    /// </summary>
    public bool IsRead { get; set; }

    /// <summary>
    /// Relationship with the User table
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Relationship with the TripReply table
    /// </summary>
    public TripReply TripReply { get; set; } = null!;
}