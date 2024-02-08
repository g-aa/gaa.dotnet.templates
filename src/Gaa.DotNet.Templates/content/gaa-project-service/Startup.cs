using Gaa.Project.Service.Consumers;
using Gaa.Project.Service.Infrastructure;
using Gaa.Project.Service.Jobs;
using Gaa.Project.Service.Security;
using MassTransit;
using Quartz;
using Quartz.AspNetCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

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
    /// Перечень сборок приложения.
    /// </summary>
    public IEnumerable<Assembly> Assemblies { get; init; }

    /// <summary>
    /// Инициализация экземпляра класса <see cref="Startup"/>.
    /// </summary>
    /// <param name="configuration">Набор свойств конфигурации приложения.</param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        Assemblies = new Assembly[]
        {
            typeof(ServiceLayer).Assembly,
        };
    }

    /// <summary>
    /// Метод конфигурации сервисов приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        #region [ Service configuration ]

        #endregion

        #region [ Infrastructure configuration ]

        services.AddScoped<ServiceUser>();
        services.AddHttpContextAccessor();
        services.AddWebApiSwaggerDocumentation(Assemblies);

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

        services.AddQuartz(configure =>
        {
            configure.SchedulerId = $"{Program.ServiceName} Scheduler: '{Guid.NewGuid()}'";
            configure.SchedulerName = $"{Program.ServiceName} Quartz.NET Scheduler";

            configure.AddJob<SampleJob>(
                SampleJob.Key,
                jobConfigurator => jobConfigurator.WithDescription("sample background job"));

            configure.AddTrigger(triggerConfigurator => triggerConfigurator
                .WithIdentity("cron trigger")
                .ForJob(SampleJob.Key)
                .WithCronSchedule("00 */2 * * * ?")
                .WithDescription("cron trigger for sample background job"));
        });

        services.AddQuartzServer(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        #endregion

        services.AddControllers(configure =>
        {
            configure.Filters.Add<ApiExceptionFilter>();
        });
        services.AddEndpointsApiExplorer();
    }

    /// <summary>
    /// Метод настройки конвейера HTTP-запросов.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
    /// <param name="environment">Окружение приложения.</param>
    public void Configure(IApplicationBuilder builder, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            builder.UseDeveloperExceptionPage();
        }

        builder.UseRouting();
        builder.UseAuthorization();

        builder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });
        });

        builder.UseSwaggerDocumentation();
    }
}