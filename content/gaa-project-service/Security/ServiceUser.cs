using System.Diagnostics.CodeAnalysis;

namespace Gaa.Project.Service.Security;

/// <summary>
/// Пользователь сервиса.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class ServiceUser
{
    /// <summary>
    /// Наименование пользователя по умолчанию.
    /// </summary>
    public const string DefaultName = "root-user";

    /// <summary>
    /// Инициализатор экземпляра класса <see cref="ServiceUser"/>.
    /// </summary>
    /// <param name="httpContextAccessor">Механизм доступа к HTTP контексту.</param>
    public ServiceUser(IHttpContextAccessor httpContextAccessor)
    {
        var principal = httpContextAccessor.HttpContext?.User;
        this.Name = principal?.Identity?.Name ?? DefaultName;
    }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string Name { get; init; }
}