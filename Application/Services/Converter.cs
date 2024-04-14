using System.Diagnostics.CodeAnalysis;
using Application.Contracts;
using Application.Exceptions;
using Models;

namespace Application.Services;

public static class Converter
{
    [return: NotNullIfNotNull("application")]
    public static ApplicationDto? ToDto(Models.Application? application)
    {
        if (application is null)
        {
            return null;
        }

        return new ApplicationDto(
            application.Id,
            application.AuthorId,
            application.Activity?.ToString(),
            application.Name,
            application.Description,
            application.Outline);
    }

    public static IEnumerable<ApplicationDto> ToDto(IEnumerable<Models.Application> applications)
        => applications.Select(ToDto)!;

    public static ApplicationDto WithId(ApplicationNoIdDto applicationNoIdDto, Guid id)
    {
        ArgumentNullException.ThrowIfNull(applicationNoIdDto);

        if (applicationNoIdDto.Author is null)
        {
            throw WithHttpCodeException.NewBadRequest("Author must be set");
        }

        return new ApplicationDto(
            id,
            applicationNoIdDto.Author.Value,
            applicationNoIdDto.Activity,
            applicationNoIdDto.Name,
            applicationNoIdDto.Description,
            applicationNoIdDto.Outline);
    }

    public static Models.Application ToEntity(
        ApplicationDto applicationDto,
        DateTime createdTime,
        DateTime? submittedTime = null)
    {
        ArgumentNullException.ThrowIfNull(applicationDto);
        return new Models.Application(
            applicationDto.Id,
            applicationDto.Author,
            createdTime,
            ToActivity(applicationDto.Activity),
            applicationDto.Name,
            applicationDto.Description,
            applicationDto.Outline,
            submittedTime);
    }

    public static Activity? ToActivity(string? activityType)
        => activityType is null
            ? null
            : Enum.TryParse(activityType, true, out Activity result)
                ? result
                : throw WithHttpCodeException.NewBadRequest($"Invalid activity: {activityType}");
}