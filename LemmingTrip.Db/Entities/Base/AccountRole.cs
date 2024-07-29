namespace LemmingTrip.Db.Entities.Base;

/// <summary>
/// Роль аккаунта пользователя
/// </summary>
public enum AccountRole
{
    /// <summary>
    /// Простой пользователь
    /// </summary>
    Tourist,

    /// <summary>
    /// Гид
    /// </summary>
    Guide,

    /// <summary>
    /// Администратор
    /// </summary>
    Administrator
}