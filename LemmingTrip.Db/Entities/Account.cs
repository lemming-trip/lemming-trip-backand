using LemmingTrip.Db.Entities.Base;

namespace LemmingTrip.Db.Entities;

/// <summary>
/// User's account
/// </summary>
public class Account
{
    /// <summary>
    /// Account id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Account provider. Google, Facebook, etc.
    /// </summary>
    public AccountProvider AccountProvider { get; set; }

    /// <summary>
    /// Password (encrypted). Only for local authentication
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Solt (for password hashing). Only for local authentication
    /// </summary>
    public int Salt { get; set; }

    /// <summary>
    /// Activation code. Only for local authentication
    /// </summary>
    public Guid ActivationCode { get; set; }

    /// <summary>
    /// Is the account verified? After confirmation of the email for local authentication, or
    /// OAuth authentication.
    /// </summary>
    public Boolean IsVerified { get; set; }
    
    /// <summary>
    /// Last login date
    /// </summary>
    public DateTime? LastLoginAt { get; set; }

    /// <summary>
    /// Relationship with User table
    /// </summary>
    public User User { get; set; } = null!;
}