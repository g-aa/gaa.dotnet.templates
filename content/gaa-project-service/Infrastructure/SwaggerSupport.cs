using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Gaa.Project.Service.Infrastructure;

/// <summary>
/// Регистрация и настройка swagger в приложении.
/// </summary>
[ExcludeFromCodeCoverage]
public static class SwaggerSupport
{
    /// <summary>
    /// Регистрация swagger UI в коллекции сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="assemblies">Перечень сборок проекта.</param>
    /// <returns>Модифицированная коллекция сервисов.</returns>
    public static IServiceCollection AddWebApiSwaggerDocumentation(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = $"{Program.ServiceName} {Program.CurrentVersion}",
                Version = "v1",
                Description = $"Rest API для взаимодействия с функционалом {Program.ServiceName}.",
            });

            options.EnableAnnotations();
            options.OperationFilter<SwaggerResponseOperationFilter>();

            foreach (var assembly in assemblies)
            {
                var xmlDocument = $"{assembly.GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlDocument));
            }

            options.SchemaGeneratorOptions.SchemaIdSelector = (Type type) => type.Name;
        });

        return services;
    }

    /// <summary>
    /// Инициализация swagger UI.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
    /// <returns>Модифицированный строитель приложения.</returns>
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder builder)
    {
        builder.UseSwagger();
        builder.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Program.ServiceName} API v1");
            options.DisplayRequestDuration();
        });

        return builder;
    }
}