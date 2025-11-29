using FluentValidation;

using MediatR.Pipeline;

using System.Diagnostics.CodeAnalysis;

namespace Gaa.Project.Logic.Validators;

/// <summary>
/// Препроцессор валидации.
/// </summary>
/// <typeparam name="TRequest">Тип запроса.</typeparam>
[ExcludeFromCodeCoverage]
public sealed class ValidationRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly IValidator<TRequest>? _validator;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ValidationRequestPreProcessor{TRequest}"/>.
    /// </summary>
    /// <param name="validator">Валидатор данных запроса.</param>
    public ValidationRequestPreProcessor(
        IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    /// <inheritdoc />
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        if (_validator == null)
        {
            return Task.CompletedTask;
        }

        return _validator.ValidateAndThrowAsync(request, cancellationToken);
    }
}