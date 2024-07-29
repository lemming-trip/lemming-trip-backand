namespace LemmingTrip.Db.Entities.Base;

/// <summary>
/// Тип путешествия
/// </summary>
public enum TripType
{
    /// <summary>
    /// Индивидуальное путешествие
    /// </summary>
    Individual,
    /// <summary>
    /// Путешествие в группе
    /// </summary>
    Group,
    /// <summary>
    /// Индивидуальное или групповое путешествие
    /// </summary>
    IndividualOrGroup
}