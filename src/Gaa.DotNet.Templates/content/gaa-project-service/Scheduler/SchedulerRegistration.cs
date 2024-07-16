using System.Diagnostics.CodeAnalysis;

using Quartz;
using Quartz.AspNetCore;

namespace Gaa.Project.Service.Scheduler;

/// <summary>
/// Регистрация компонентов Quartz.NET.
/// </summary>
[ExcludeFromCodeCoverage]
public static class SchedulerRegistration
{
    /// <summary>
    /// Добавляет компоненты Quartz.NET.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <returns>Модифицированная коллекция сервисов.</returns>
    public static IServiceCollection AddScheduler(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddQuartz(q =>
            {
                q.SchedulerId = $"Scheduler: {Guid.NewGuid()}";
                q.SchedulerName = "Service Scheduler";

                q.AddJob<SampleJob>(
                    SampleJob.Key,
                    jobConfigurator => jobConfigurator.WithDescription("sample background job"));

                q.AddTrigger(triggerConfigurator => triggerConfigurator
                    .WithIdentity("cron trigger")
                    .ForJob(SampleJob.Key)
                    .WithCronSchedule("00 */2 * * * ?")
                    .WithDescription("cron trigger for sample background job"));
            })
            .AddQuartzServer(q =>
            {
                q.WaitForJobsToComplete = true;
            });
    }
}