using NLog;
using NLog.Web;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Gaa.Project.Service;

/// <summary>
/// Базовый класс приложения.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Program
{
    /// <summary>
    /// Наименование сервиса.
    /// </summary>
    public const string ServiceName = "Web Service";

    /// <summary>
    /// Версия приложения.
    /// </summary>
    public static string CurrentVersion { get; private set; }

    /// <summary>
    /// Инициализация статических параметров класса <see cref="Program"/>.
    /// </summary>
    static Program()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
        CurrentVersion = $"v{fvi.FileVersion}";
    }

    /// <summary>
    /// Точка входа в приложение.
    /// </summary>
    /// <param name="args">Аргументы запуска приложения.</param>
    public static async Task Main(string[] args)
    {
        var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        try
        {
            logger.Debug("{0} '{1}' - запущен на выполнение...", ServiceName, CurrentVersion);
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }
        catch (Exception exception)
        {
            logger.Error(exception, "{0} '{1}' - остановлен из за перехвата не необработанного исключения!", ServiceName, CurrentVersion);
        }
        finally
        {
            logger.Debug("{0} '{1}' - остановлен.", ServiceName, CurrentVersion);
            LogManager.Shutdown();
        }
    }

    /// <summary>
    /// Инициализация приложения.
    /// </summary>
    /// <param name="args">Аргументы запуска приложения.</param>
    /// <returns>Строитель приложения.</returns>
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureLogging((context, logging) =>
            {
                /*
                * var nLogSection = context.Configuration.GetSection("NLog"); // пока уберем
                * LogManager.Configuration = new NLogLoggingConfiguration(nLogSection); // пока уберем
                */
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                /*
                 * logging.AddNLogWeb(); // пока уберем
                 */
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseNLog();
    }
}