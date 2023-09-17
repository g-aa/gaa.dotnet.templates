using Gaa.Project.Service.Security;

namespace Gaa.Project.Service.Extensions;

/// <summary>
/// Методы расширения для <see cref="ILogger"/>.
/// </summary>
public static class LoggerExtensions
{
    /// <summary>
    /// Добавить компоненты <see cref="ServiceUser"/> к <see cref="ILogger"/>.
    /// </summary>
    /// <typeparam name="TLogger"></typeparam>
    /// <param name="logger">Журнал логирования.</param>
    /// <param name="user">Пользователь сервиса.</param>
    /// <returns><see cref="IDisposable"/> используемый для освобождения ресурсов.</returns>
    public static IDisposable BeginWithServiceUserScope<TLogger>(this TLogger logger, ServiceUser user)
        where TLogger : notnull, ILogger
    {
        return logger.BeginScope(new[]
        {
            new KeyValuePair<string, object>("Service-user", user.Name),
        });
    }
}