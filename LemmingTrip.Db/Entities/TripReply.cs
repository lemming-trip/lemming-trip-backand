namespace LemmingTrip.Db.Entities;

/// <summary>
/// Reply to trip
/// </summary>
public class TripReply
{
    /// <summary>
    /// Trip reply id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Trip id
    /// </summary>
    public Guid TripId { get; set; }

    /// <summary>
    /// User id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Text of the reply
    /// </summary>
    public string Text { get; set; } = null!;

    /// <summary>
    /// Additional images
    /// </summary>
    public IList<string>? Images { get; set; }

    /// <summary>
    /// Video link
    /// </summary>
    public string? VideoLink { get; set; }
    
    /// <summary>
    /// Link to the reply
    /// </summary>
    public IList<TripReplyMessage>? TripReplyMessages { get; set; }

    /// <summary>
    /// Relationship with the Trip table
    /// </summary>
    public Trip Trip { get; set; } = null!;
    
    /// <summary>
    /// Relationship with the User table
    /// </summary>
    public User User { get; set; } = null!;
}