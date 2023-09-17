using Gaa.Project.Service.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Gaa.Project.Service.Infrastructure;

/// <summary>
/// Swagger core response type filter.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class SwaggerResponseOperationFilter : IOperationFilter
{
    private static readonly IReadOnlyDictionary<int, string> ErrorStatuses = new Dictionary<int, string>
    {
        { StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации." },
        { StatusCodes.Status401Unauthorized, "Пользователь не авторизован." },
        { StatusCodes.Status403Forbidden, "Доступ к ресурсу запрещенный." },
        { StatusCodes.Status422UnprocessableEntity, "Внутренняя ошибка сервера." },
    };

    /// <inheritdoc />
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (typeof(AboutController).Equals(context.MethodInfo.DeclaringType))
        {
            return;
        }

        var mediaType = new OpenApiMediaType
        {
            Schema = context.SchemaGenerator.GenerateSchema(typeof(ProblemDetails), context.SchemaRepository),
        };

        var content = new Dictionary<string, OpenApiMediaType>
        {
            { "text/plain", mediaType },
            { "application/json", mediaType },
            { "text/json", mediaType },
        };

        foreach (var status in ErrorStatuses)
        {
            var httpCode = status.Key.ToString(CultureInfo.InvariantCulture);
            if (!operation.Responses.ContainsKey(httpCode))
            {
                operation.Responses.Add(httpCode, new OpenApiResponse { Content = content, Description = status.Value, });
            }
        }
    }
}