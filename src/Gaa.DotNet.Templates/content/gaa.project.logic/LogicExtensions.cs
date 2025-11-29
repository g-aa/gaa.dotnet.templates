using System.Diagnostics.CodeAnalysis;

using FluentValidation;

using Gaa.Project.Logic.Validators;

using Microsoft.Extensions.DependencyInjection;

namespace Gaa.Project.Logic;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection" />.
/// </summary>
[ExcludeFromCodeCoverage]
public static class LogicExtensions
{
    /// <summary>
    /// Добавляет компоненты логики в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Модифицированная коллекция сервисов.</returns>
    public static IServiceCollection AddLogicServices(this IServiceCollection services)
    {
        return services
            .AddValidatorsFromAssemblyContaining<LogicLayer>()
            .AddMediatR(configuration =>
            {
                configuration
                    .RegisterServicesFromAssemblyContaining<LogicLayer>()
                    .AddOpenRequestPreProcessor(typeof(ValidationRequestPreProcessor<>));
            });
    }
}