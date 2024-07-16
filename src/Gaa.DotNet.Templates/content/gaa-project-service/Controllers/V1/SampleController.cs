using Asp.Versioning;

using Gaa.Project.Service.Models;

using MassTransit;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace Gaa.Project.Service.Controllers.V1;

/// <summary>
/// Пример контроллер по отправке сообщений в очередь.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/sample")]
public sealed class SampleController : ControllerBase
{
    private readonly IPublishEndpoint _endpoint;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="SampleController"/>.
    /// </summary>
    /// <param name="endpoint">MassTransit publish endpoint.</param>
    public SampleController(IPublishEndpoint endpoint)
    {
        _endpoint = endpoint;
    }

    /// <summary>
    /// Отправляет сообщение в очередь.
    /// </summary>
    /// <returns>Результат работы асинхронной задачи.</returns>
    [HttpPost("message")]
    [SwaggerResponse(StatusCodes.Status200OK, "Сообщение отправлено.")]
    public Task PostMessage([FromBody] SampleMessageModel model, CancellationToken cancellationToken)
    {
        return _endpoint.Publish(model, cancellationToken);
    }
}