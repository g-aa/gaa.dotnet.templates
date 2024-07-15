using Quartz;

namespace Gaa.Project.Service.Scheduler;

/// <summary>
/// Пример фоновой задачи.
/// </summary>
[DisallowConcurrentExecution]
public sealed class SampleJob : IJob
{
    /// <inheritdoc cref="JobKey"/>
    public static readonly JobKey Key = new("sample-job", "examples");

    private readonly ILogger<SampleJob> _logger;

    /// <summary>
    /// Инициализация нового экземпляра класса <see cref="SampleJob"/>.
    /// </summary>
    /// <param name="logger">Журнал сообщений.</param>
    public SampleJob(ILogger<SampleJob> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogWarning("Сообщение из фонового задания '{Name}'.", Key.Name);
        return Task.CompletedTask;
    }
}