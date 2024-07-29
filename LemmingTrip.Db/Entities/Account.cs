using LemmingTrip.Db.Entities.Base;

namespace LemmingTrip.Db.Entities;

/// <summary>
/// Аккаунт пользователя
/// </summary>
public class Account
{
    /// <summary>
    /// Идентификатор аккаунта
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// Почтовый адрес
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Пароль (зашифрован)
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Соль, первичная. К этой соли подмешивается специальный код.
    /// </summary>
    public int Salt { get; set; }

    /// <summary>
    /// Роль аккаунта пользователя
    /// </summary>
    public AccountRole AccountRole { get; set; }
    
    /// <summary>
    /// Активен ли аккаунт
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Код активации. Отправляется по email для подтверждения регистрации.
    /// </summary>
    public Guid ActivationCode { get; set; }
    
    /// <summary>
    /// Дата регистрации
    /// </summary>
    public DateTime RegistrationDate { get; set; }

    /// <summary>
    /// Связь с таблицей User
    /// </summary>
    public User? User { get; set; }
}