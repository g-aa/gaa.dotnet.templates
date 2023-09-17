using Quartz;

namespace Gaa.Project.Service.Jobs;

/// <summary>
/// Пример фоновой задачи.
/// </summary>
[DisallowConcurrentExecution]
public sealed class SampleJob : IJob
{
    /// <inheritdoc cref="JobKey"/>
    public static readonly JobKey Key = new("sample-job", "examples");

    private readonly ILogger<SampleJob> logger;

    /// <summary>
    /// Инициализация нового экземпляра класса <see cref="SampleJob"/>.
    /// </summary>
    /// <param name="logger">Журнал сообщений.</param>
    public SampleJob(ILogger<SampleJob> logger)
    {
        this.logger = logger;
    }

    /// <inheritdoc/>
    public Task Execute(IJobExecutionContext context)
    {
        this.logger.LogWarning("Сообщение из фонового задания '{0}'.", Key.Name);
        return Task.CompletedTask;
    }
}