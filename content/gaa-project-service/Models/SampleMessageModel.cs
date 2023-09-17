namespace Gaa.Project.Service.Models;

/// <summary>
/// Пример сообщения.
/// </summary>
public record SampleMessageModel
{
    /// <summary>
    /// Идентификатор сообщения.
    /// </summary>
    public Guid MessageId { get; init; }

    /// <summary>
    /// Текст сообщения.
    /// </summary>
    /// <example>Текст сообщения...</example>
    public string Message { get; init; } = string.Empty;
}