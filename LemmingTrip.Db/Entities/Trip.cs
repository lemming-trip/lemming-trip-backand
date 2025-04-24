using LemmingTrip.Db.Entities.Base;
using NetTopologySuite.Geometries;

namespace LemmingTrip.Db.Entities;

/// <summary>
/// Trip
/// </summary>
public class Trip
{
    /// <summary>
    /// Trip id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Trip title
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Trim text
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Trip title image
    /// </summary>
    public string? TitleImage { get; set; }

    /// <summary>
    /// Addtional trip images
    /// </summary>
    public IList<string>? Images { get; set; }

    /// <summary>
    /// Video link
    /// </summary>
    public string? VideoLink { get; set; }

    /// <summary>
    /// Route coordinates
    /// </summary>
    public Geometry? Route { get; set; }

    /// <summary>
    /// Trip rating
    /// </summary>
    public ushort Rating { get; set; }

    /// <summary>
    /// Like count
    /// </summary>
    public uint Likes { get; set; }

    /// <summary>
    /// Trip's type
    /// </summary>
    public TripType TripType { get; set; }

    /// <summary>
    /// Trip's search type
    /// </summary>
    public TripSearchType TripSearchType { get; set; }

    /// <summary>
    /// Relationship with User table
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Relationship with TripLike table
    /// </summary>
    public IList<TripReply>? TripReplies { get; set; }
}