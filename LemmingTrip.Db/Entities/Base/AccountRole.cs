namespace LemmingTrip.Db.Entities.Base;

/// <summary>
/// Role of the user's account
/// </summary>
public enum AccountRole
{
    /// <summary>
    /// Simple user (Tourist)
    /// </summary>
    Tourist,
    /// <summary>
    /// Guide user
    /// </summary>
    Guide,
    /// <summary>
    /// Administrator user
    /// </summary>
    Administrator
}