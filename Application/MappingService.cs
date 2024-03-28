using System;
using Abstractions.Services;
using Models;

namespace Application;

public static class MappingService
{
    public static ApplicationDto AsDto(this Models.Application? application)
    {
        ArgumentNullException.ThrowIfNull(application);
        return new ApplicationDto(
            application.Id,
            application.AuthorId,
            application.Activity?.ToString(),
            application.Name,
            application.Description,
            application.Outline);
    }

    public static Activity? FromString(this string? activityType)
        => Enum.TryParse(activityType, true, out Activity result) ? result : null;
}