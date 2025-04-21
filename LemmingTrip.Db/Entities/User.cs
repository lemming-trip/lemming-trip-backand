using LemmingTrip.Db.Entities.Base;

namespace LemmingTrip.Db.Entities;

/// <summary>
/// User
/// </summary>
public class User
{
    /// <summary>
    /// User id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Email address
    /// </summary>
    public string Email { get; set; } = null!;
    
    /// <summary>
    /// Is the account active?
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// User's account role
    /// </summary>
    public UserRole AccountRole { get; set; }

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
    /// Date of registration
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date of last update
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Date of last visit
    /// </summary>
    public DateTime LastSeenAt { get; set; }

    /// <summary>
    /// Relationship with the Account table
    /// </summary>
    public ICollection<Account> Accounts { get; set; } = null!;

    /// <summary>
    /// Relationship with the Trip table
    /// </summary>
    // public ICollection<Trip> Trips { get; } = new List<Trip>();
}