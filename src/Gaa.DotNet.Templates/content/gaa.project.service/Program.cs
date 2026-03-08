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

            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders().SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace).AddNLog();
            Startup.ConfigureServices(builder.Services, builder.Configuration);

            var host = builder.Build();
            Startup.Configure(host, host.Environment);

            await host.RunAsync();
        }
        catch (Exception ex)
        {
            log.Error(ex, "Сервис остановлен из за перехвата не необработанного исключения!");
        }
        finally
        {
            log.Info("Сервис остановлен.");
            LogManager.Shutdown();
        }
    }
}