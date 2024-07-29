
namespace LemmingTrip.Db.Entities.Base;

/// <summary>
/// Тип поиска для путешествия
/// </summary>
public enum TripSearchType
{
    /// <summary>
    /// Ищу команду
    /// </summary>
    Team,
    /// <summary>
    /// Ищу гида
    /// </summary>
    Guide,
    /// <summary>
    /// Ищу спонсора
    /// </summary>
    Sponsor
}