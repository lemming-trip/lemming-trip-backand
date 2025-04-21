namespace LemmingTrip.Db.Entities.Base;

/// <summary>
/// OAuth provider
/// </summary>
public enum AccountProvider
{
    /// <summary>
    /// Internal user account
    /// </summary>
    Local,

    /// <summary>
    /// Google OAuth provider
    /// </summary>
    Google,

    /// <summary>
    /// Facebook OAuth provider
    /// </summary>
    Facebook,

    /// <summary>
    /// Microsoft OAuth provider
    /// </summary>
    Microsoft,

    /// <summary>
    /// Apple OAuth provider
    /// </summary>
    Apple
}