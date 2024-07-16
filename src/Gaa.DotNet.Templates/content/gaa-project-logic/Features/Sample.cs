using FluentValidation;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Gaa.Project.Logic.Features;

/// <summary>
/// Пример.
/// </summary>
public static class Sample
{
    /// <summary>
    /// Команда.
    /// </summary>
    public sealed record Command : IRequest
    {
        /// <summary>
        /// Сообщение.
        /// </summary>
        public string? Message { get; init; }
    }

    /// <summary>
    /// Валидатор для <see cref="Command"/>.
    /// </summary>
    public sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализирует новы экземпляр класса <see cref="Validator"/>.
        /// </summary>
        public Validator()
        {
            RuleFor(c => c.Message)
                .Length(2, 16)
                .When(c => c.Message != null);
        }
    }

    /// <summary>
    /// Обработчик.
    /// </summary>
    public sealed class Handler : IRequestHandler<Command>
    {
        private readonly ILogger<Handler> _logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал регистрации сообщений.</param>
        public Handler(ILogger<Handler> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public Task Handle(Command request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Получено и обработано сообщение '{Message}'.", request.Message);
            return Task.CompletedTask;
        }
    }
}