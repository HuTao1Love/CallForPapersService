namespace Abstractions.Services;

public record ApplicationNoIdDto(
    Guid? Author,
    string? Activity,
    string? Name,
    string? Description,
    string? Outline);