using System.Diagnostics.CodeAnalysis;

using Gaa.Project.Service.Consumers;
using Gaa.Project.Service.Infrastructure;
using Gaa.Project.Service.Scheduler;
using Gaa.Project.Service.Security;

using MassTransit;

using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Gaa.Project.Service;

/// <summary>
/// Класс инициализирующий приложение.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class Startup
{
    /// <inheritdoc cref="IConfiguration"/>
    public IConfiguration Configuration { get; init; }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Startup"/>.
    /// </summary>
    /// <param name="configuration">Набор свойств конфигурации приложения.</param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// Конфигурирует сервисы приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddScoped<ServiceUser>()
            .AddHttpContextAccessor()
            .AddApiVersioningSupport()
            .AddSwaggerDocumentation()
            .AddScheduler(Configuration);

        var uriBuilder = new UriBuilder(Configuration.GetConnectionString("RabbitMQ")!);
        services.AddMassTransit(configure =>
        {
            configure.AddConsumer<SampleConsumer>();

            configure.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(uriBuilder.Uri, hostConfigurator =>
                {
                    hostConfigurator.Username(uriBuilder.UserName);
                    hostConfigurator.Password(uriBuilder.Password);
                });

                configurator.ConfigureEndpoints(context);
            });
        });

        services
            .AddControllers(options => options.Filters.Add<ApiExceptionFilter>());

        services
            .AddOpenTelemetry()
            .WithMetrics(meterBuilder => meterBuilder
                .AddRuntimeInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddPrometheusExporter());

        services
            .Configure<PrometheusAspNetCoreOptions>(options => options.DisableTotalNameSuffixForCounters = true);
    }

    /// <summary>
    /// Настраивает конвейер HTTP-запросов.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
    /// <param name="environment">Окружение приложения.</param>
    public void Configure(IApplicationBuilder builder, IWebHostEnvironment environment)
    {
        builder
            .UseDiagnostics()
            .UseRouting()
            .UseAuthorization()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger");
                    return Task.CompletedTask;
                });
            })
            .UseSwaggerDocumentation();
    }
}