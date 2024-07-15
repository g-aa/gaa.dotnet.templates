using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

using FluentValidation;

using Gaa.Project.Service.Extensions;
using Gaa.Project.Service.Security;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gaa.Project.Service.Infrastructure;

/// <summary>
/// Обработчик исключений.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class ApiExceptionFilter : IExceptionFilter
{
    /// <inheritdoc />
    public void OnException(ExceptionContext context)
    {
        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ApiExceptionFilter>>();
        var user = context.HttpContext.RequestServices.GetRequiredService<ServiceUser>();

        using var scope = logger.BeginWithServiceUserScope(user);
        switch (context.Exception)
        {
            case ValidationException validationException:
                logger.LogWarning(validationException, validationException.Message);
                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(k => k.Key, v => v.Select(e => e.ErrorMessage).ToArray());
                var validationError = new ValidationProblemDetails(errors);
                context.Result = new BadRequestObjectResult(validationError);
                break;

            case DbException dbException:
                logger.LogError(dbException, dbException.Message);
                var dbErrorDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "Database error.",
                };

                context.Result = new ObjectResult(dbErrorDetails)
                {
                    StatusCode = dbErrorDetails.Status,
                };
                break;

            default:
                logger.LogCritical(context.Exception, context.Exception.Message);
                var criticalDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "Internal server error.",
                };

                context.Result = new ObjectResult(criticalDetails)
                {
                    StatusCode = criticalDetails.Status,
                };
                break;
        }
    }
}