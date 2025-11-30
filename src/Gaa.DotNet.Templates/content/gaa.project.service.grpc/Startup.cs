using System.Diagnostics.CodeAnalysis;

using Gaa.Project.Service.Grpc.Infrastructure;
using Gaa.Project.Service.Grpc.Services;

namespace Gaa.Project.Service.Grpc;

#pragma warning disable S2325 // Methods and properties that don't access instance data should be static

/// <summary>
/// Класс инициализирующий приложение.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class Startup
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Startup"/>.
    /// </summary>
    /// <param name="configuration">Набор свойств конфигурации приложения.</param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <inheritdoc cref="IConfiguration"/>
    public IConfiguration Configuration { get; init; }

    /// <summary>
    /// Конфигурирует сервисы приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddGrpc(options =>
            {
                options.Interceptors.Add<ServiceExceptionInterceptor>();
            });
    }

    /// <summary>
    /// Настраивает конвейер HTTP-запросов.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
    /// <param name="environment">Окружение приложения.</param>
    public void Configure(IApplicationBuilder builder, IWebHostEnvironment environment)
    {
        builder
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", () => "This is a gRPC Service!");
                endpoints.MapGrpcService<AboutService>();
                endpoints.MapGrpcService<ExampleService>();
            });
    }
}