using Microsoft.AspNetCore;

using NLog;
using NLog.Web;

using System.Diagnostics.CodeAnalysis;

namespace Gaa.Project.Service;

/// <summary>
/// Базовый класс приложения.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Program
{
    /// <summary>
    /// Точка входа в приложение.
    /// </summary>
    /// <param name="args">Аргументы запуска приложения.</param>
    public static async Task Main(string[] args)
    {
        var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        try
        {
            logger.Debug("Сервис запущен на выполнение...");
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }
        catch (Exception exception)
        {
            logger.Error(exception, "Сервис остановлен из за перехвата не необработанного исключения!");
        }
        finally
        {
            logger.Debug("Сервис остановлен.");
            LogManager.Shutdown();
        }
    }

    /// <summary>
    /// Инициализация приложения.
    /// </summary>
    /// <param name="args">Аргументы запуска приложения.</param>
    /// <returns>Строитель приложения.</returns>
    public static IWebHostBuilder CreateHostBuilder(string[] args)
    {
        return WebHost
            .CreateDefaultBuilder<Startup>(args)
            .ConfigureLogging((context, logging) =>
            {
                logging
                    .ClearProviders()
                    .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)
                    .AddNLogWeb();
            });
    }
}