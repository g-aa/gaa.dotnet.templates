using System.Diagnostics;

using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace Gaa.Project.Service.Controllers;

/// <summary>
/// Контроллер информации о приложении.
/// </summary>
[ApiVersionNeutral]
[Route("api/about")]
public sealed class AboutController : ControllerBase
{
    /// <summary>
    /// Получает версию приложения.
    /// </summary>
    /// <returns>Версия приложения.</returns>
    [HttpGet("version")]
    [SwaggerResponse(StatusCodes.Status200OK, "Версия приложения.", typeof(IReadOnlyDictionary<string, string?>))]
    public IReadOnlyDictionary<string, string?> Version()
    {
        var assembly = typeof(AboutController).Assembly;
        var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
        return new Dictionary<string, string?>
        {
            { "Version", assembly.GetName().Version?.ToString(3) },
            { "FileVersion", fvi.FileVersion },
            { "ProductVersion", fvi.ProductVersion },
        };
    }
}