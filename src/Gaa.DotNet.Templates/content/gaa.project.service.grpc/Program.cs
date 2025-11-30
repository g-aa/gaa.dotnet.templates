using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore;

using NLog;
using NLog.Extensions.Logging;

namespace Gaa.Project.Service.Grpc;

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
    /// <returns>Результат выполнения асинхронной задачи.</returns>
    public static async Task Main(string[] args)
    {
        var log = LogManager.Setup().LoadConfigurationFromFile().GetCurrentClassLogger();

        try
        {
            log.Info("Приложение запущено на выполнение...");
            log.Trace("Идентификатор процесса: '{Id}'.", Environment.ProcessId);
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }
        catch (Exception ex)
        {
            log?.Fatal(ex, "Приложение остановлено из за перехвата не необработанного исключения!");
        }
        finally
        {
            log?.Info("Приложение остановлено.");
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