namespace LemmingTrip.Db.Entities;

/// <summary>
/// User
/// </summary>
public class User
{
    /// <summary>
    /// User id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Avatar image
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// Phone
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// City
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Address
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// First name
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Middle name
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Date of birth
    /// </summary>
    public DateTime? DateBirth { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Relationship with the Account table
    /// </summary>
    public Account Account { get; set; } = null!;

    /// <summary>
    /// Relationship with the Trip table
    /// </summary>
    public ICollection<Trip> Trips { get; } = new List<Trip>();
}