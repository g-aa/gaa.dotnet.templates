using System.ComponentModel.DataAnnotations;

namespace Gaa.Project.Service.Models;

/// <summary>
/// Пример сообщения.
/// </summary>
public record SampleMessageModel
{
    /// <summary>
    /// Идентификатор сообщения.
    /// </summary>
    [Required]
    public Guid MessageId { get; init; }

    /// <summary>
    /// Текст сообщения.
    /// </summary>
    /// <example>Текст сообщения...</example>
    [Required]
    [StringLength(32, MinimumLength = 0)]
    public string Message { get; init; } = string.Empty;
}