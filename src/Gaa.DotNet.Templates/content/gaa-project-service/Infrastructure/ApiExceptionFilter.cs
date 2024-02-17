using Gaa.Project.Service.Extensions;
using Gaa.Project.Service.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Gaa.Project.Service.Infrastructure;

/// <summary>
/// Обработчик исключений.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class ApiExceptionFilter : IExceptionFilter
{
    /// <summary>
    /// Передавать детализацию об исключениях. 
    /// </summary>
    private readonly bool _passDetails;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="ApiExceptionFilter"/>.
    /// </summary>
    public ApiExceptionFilter()
    {
        _passDetails = false;
    }

    /// <inheritdoc />
    public void OnException(ExceptionContext context)
    {
        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ApiExceptionFilter>>();
        var user = context.HttpContext.RequestServices.GetRequiredService<ServiceUser>();
        var exception = context.Exception;

        using var scope = logger.BeginWithServiceUserScope(user);
        switch (exception)
        {
            case DbException dbException:
                logger.LogError(dbException, dbException.Message);
                var dbErrorDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = _passDetails ? exception.Message : "Database error.",
                };

                context.Result = new ObjectResult(dbErrorDetails)
                {
                    StatusCode = dbErrorDetails.Status,
                };
                break;

            default:
                logger.LogCritical(exception, exception.Message);
                var criticalDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = _passDetails ? exception.Message : "Internal server error.",
                };

                context.Result = new ObjectResult(criticalDetails)
                {
                    StatusCode = criticalDetails.Status,
                };
                break;
        }
    }
}