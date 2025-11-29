using Microsoft.AspNetCore;

using NLog;
using NLog.Extensions.Logging;

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
        var log = LogManager.Setup().LoadConfigurationFromFile().GetCurrentClassLogger();

        try
        {
            log.Info("Сервис запущен на выполнение...");
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }
        catch (Exception exception)
        {
            log.Error(exception, "Сервис остановлен из за перехвата не необработанного исключения!");
        }
        finally
        {
            log.Info("Сервис остановлен.");
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
            .ConfigureLogging((_, logging) =>
            {
                logging
                    .ClearProviders()
                    .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)
                    .AddNLog();
            });
    }
}