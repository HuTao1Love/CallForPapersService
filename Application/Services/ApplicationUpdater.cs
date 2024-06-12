using Application.Contracts;
using Application.Exceptions;

namespace Application.Services;

public static class ApplicationUpdater
{
    public static void UpdateStatus(Models.Application application, ApplicationNoIdDto update)
    {
        ArgumentNullException.ThrowIfNull(application);
        ArgumentNullException.ThrowIfNull(update);

        if (update.Author is not null && !update.Author.Equals(application.AuthorId))
        {
            throw WithHttpCodeException.NewBadRequest("Author cannot be changed");
        }

        if (application.SubmittedTime is not null)
        {
            throw WithHttpCodeException.NewBadRequest("Application is submitted");
        }

        application.Name = UpdateIfNotNull(application.Name, update.Name);
        application.Description = UpdateIfNotNull(application.Description, update.Description);
        application.Outline = UpdateIfNotNull(application.Outline, update.Outline);
        application.Activity = Converter.ToActivity(
            UpdateIfNotNull(application.Activity.ToString(), update.Activity));
    }

    private static string? UpdateIfNotNull(string? src, string? value)
    {
        if (value is null)
        {
            return src;
        }

        return string.IsNullOrWhiteSpace(value) || value.Equals("null", StringComparison.OrdinalIgnoreCase)
            ? null
            : value;
    }
}