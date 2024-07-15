using System.Diagnostics.CodeAnalysis;

using Asp.Versioning;
using Asp.Versioning.ApiExplorer;

using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Gaa.Project.Service.Infrastructure;

/// <summary>
/// Регистрация и настройка swagger в приложении.
/// </summary>
[ExcludeFromCodeCoverage]
public static class SwaggerSupport
{
    /// <summary>
    /// Регистрирует поддержку версий API в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Модифицированная коллекция сервисов.</returns>
    public static IServiceCollection AddApiVersioningSupport(this IServiceCollection services)
    {
        return services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
            .Services;
    }

    /// <summary>
    /// Регистрирует swagger ui в коллекции сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Модифицированная коллекция сервисов.</returns>
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        return services
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>()
            .AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.OperationFilter<SwaggerResponseOperationFilter>();

                var xmlDocumentation = $"{typeof(ServiceLayer).Assembly.GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlDocumentation));

                options.SchemaGeneratorOptions.SchemaIdSelector = (Type type) => type.Name;
            });
    }

    /// <summary>
    /// Инициализирует swagger ui.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
    /// <returns>Модифицированный строитель приложения.</returns>
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder builder)
    {
        var apiVersionProvider = builder.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
        return builder
            .UseSwagger(options =>
            {
                options.RouteTemplate = "swagger/{documentName}/service.{json|yaml}";
            })
            .UseSwaggerUI(uiOptions =>
            {
                foreach (var groupName in apiVersionProvider.ApiVersionDescriptions.Select(e => e.GroupName))
                {
                    uiOptions.SwaggerEndpoint($"/swagger/{groupName}/service.yaml", $"REST API {groupName}");
                }

                uiOptions.DisplayRequestDuration();
                uiOptions.DefaultModelsExpandDepth(0);
            });
    }
}