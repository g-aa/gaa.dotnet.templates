using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Gaa.Project.Service.Controllers;

/// <summary>
/// Контроллер информации о приложении.
/// </summary>
[Route("api/about")]
public sealed class AboutController : ControllerBase
{
    /// <summary>
    /// Получить версию приложения.
    /// </summary>
    /// <returns>Версия приложения.</returns>
    [HttpGet("version")]
    [SwaggerResponse(StatusCodes.Status200OK, "Версия приложения.", typeof(string))]
    public string? Version()
    {
        return Program.CurrentVersion;
    }
}