using System;
using Abstractions.Services;
using Models;
using Models.Exceptions;

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

    public static ApplicationDto WithId(this ApplicationNoIdDto applicationNoIdDto, Guid id, Guid author)
    {
        ArgumentNullException.ThrowIfNull(applicationNoIdDto);

        return new ApplicationDto(
            id,
            author,
            applicationNoIdDto.Activity,
            applicationNoIdDto.Name,
            applicationNoIdDto.Description,
            applicationNoIdDto.Outline);
    }

    public static Models.Application AsEntity(this ApplicationDto applicationDto, DateTime createdTime, DateTime? submittedTime = null)
    {
        ArgumentNullException.ThrowIfNull(applicationDto);
        return new Models.Application(
            applicationDto.Id,
            applicationDto.Author,
            createdTime,
            applicationDto.Activity.FromString(),
            applicationDto.Name,
            applicationDto.Description,
            applicationDto.Outline,
            submittedTime);
    }

    public static Activity? FromString(this string? activityType)
        => activityType is null
            ? null
            : Enum.TryParse(activityType, true, out Activity result)
                ? result
                : throw new InvalidActivityException(activityType);
}