using Gaa.Project.Service.Models;
using MassTransit;

namespace Gaa.Project.Service.Consumers;

/// <summary>
/// Пример обработчика сообщений из очереди.
/// </summary>
public sealed class SampleConsumer : IConsumer<SampleMessageModel>
{
    private readonly ILogger<SampleConsumer> logger;

    /// <summary>
    /// Инициализация нового экземпляра класса <see cref="SampleConsumer"/>.
    /// </summary>
    /// <param name="logger">Журнал сообщений.</param>
    public SampleConsumer(ILogger<SampleConsumer> logger)
    {
        this.logger = logger;
    }

    /// <inheritdoc/>
    public Task Consume(ConsumeContext<SampleMessageModel> context)
    {
        var model = context.Message;
        this.logger.LogWarning("Получено и обработано сообщение '{0}'.", model.Message);
        return Task.CompletedTask;
    }
}