using Models;

namespace Abstractions.Services;

public record ApplicationDto(
    Guid Id,
    Guid Author,
    string? Activity,
    string? Name,
    string? Description,
    string? Outline);