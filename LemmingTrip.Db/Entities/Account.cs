using LemmingTrip.Db.Entities.Base;

namespace LemmingTrip.Db.Entities;

/// <summary>
/// User's account
/// </summary>
public class Account
{
    /// <summary>
    /// User's account id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Email address
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Password (encrypted)
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Solt (for password hashing)
    /// </summary>
    public int Salt { get; set; }

    /// <summary>
    /// User's account role
    /// </summary>
    public AccountRole AccountRole { get; set; }
    
    /// <summary>
    /// Is account active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Activation code
    /// </summary>
    public Guid ActivationCode { get; set; }
    
    /// <summary>
    /// Registration date
    /// </summary>
    public DateTime RegistrationDate { get; set; }

    /// <summary>
    /// Relationship with User table
    /// </summary>
    public User? User { get; set; }
}