using Gaa.Project.Service.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Gaa.Project.Service.Controllers;

/// <summary>
/// Пример контроллер по отправке сообщений в очередь.
/// </summary>
[Route("api/sample")]
public sealed class SampleController : ControllerBase
{
    private readonly IPublishEndpoint endpoint;

    /// <summary>
    /// Инициализация нового экземпляра класса <see cref="SampleController"/>.
    /// </summary>
    /// <param name="endpoint">MassTransit publish endpoint.</param>
    public SampleController(IPublishEndpoint endpoint)
    {
        this.endpoint = endpoint;
    }

    /// <summary>
    /// Отправить сообщение в очередь.
    /// </summary>
    /// <returns>Результат работы асинхронной задачи.</returns>
    [HttpPost("message")]
    [SwaggerResponse(StatusCodes.Status200OK, "Сообщение отправлено.")]
    public Task PostMessage([FromBody] SampleMessageModel model, CancellationToken cancellationToken)
    {
        return this.endpoint.Publish(model, cancellationToken);
    }
}