using System.Globalization;
using Application.Contracts;
using Application.Exceptions;
using Application.Services;
using FluentValidation;

namespace Application;

public class ApplicationService(
    IApplicationRepository repository,
    IValidator<ApplicationDto> applicationDtoValidator) : IApplicationService
{
    public async Task<IEnumerable<ApplicationDto>> GetApplicationsAsync(
        string? submittedAfterString,
        string? unsubmittedOlderString,
        CancellationToken cancellationToken = default)
    {
        bool isEmptySubmitted = string.IsNullOrWhiteSpace(submittedAfterString);
        bool isEmptyUnsubmitted = string.IsNullOrWhiteSpace(unsubmittedOlderString);

        if ((isEmptySubmitted && isEmptyUnsubmitted) || (!isEmptySubmitted && !isEmptyUnsubmitted))
        {
            throw WithHttpCodeException.NewBadRequest("SubmittedAfter xor UnsubmittedOlder must be set");
        }

        if (!DateTime.TryParse(
                (submittedAfterString ?? unsubmittedOlderString)?.Trim('"'),
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var dateTime))
        {
            throw WithHttpCodeException.NewBadRequest(
                $"Invalid datetime string format. Must be {CultureInfo.InvariantCulture.DateTimeFormat}");
        }

        return Converter.ToDto(
            submittedAfterString is not null
                ? await repository.GetSubmittedApplicationsAsync(dateTime, cancellationToken)
                : await repository.GetNotSubmittedApplicationsAsync(dateTime, cancellationToken));
    }

    public async Task<ApplicationDto?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => Converter.ToDto(await repository.FindByIdAsync(id, cancellationToken));

    public async Task<ApplicationDto?> FindNotSubmittedApplicationAsync(
        Guid authorId,
        CancellationToken cancellationToken = default)
            => Converter.ToDto(await repository.FindNotSubmittedApplicationAsync(authorId, cancellationToken));

    public async Task<ApplicationDto> CreateAsync(
        ApplicationNoIdDto applicationDto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(applicationDto);

        var applicationDtoWithId = Converter.WithId(applicationDto, Guid.NewGuid());
        await applicationDtoValidator.ValidateAndThrowAsync(applicationDtoWithId, cancellationToken);

        if (await FindNotSubmittedApplicationAsync(applicationDtoWithId.Author, cancellationToken) is not null)
        {
            throw WithHttpCodeException.NewBadRequest("User already have not submitted application");
        }

        var application = Converter.ToEntity(applicationDtoWithId, DateTime.Now);
        await repository.CreateAsync(application, cancellationToken);

        return applicationDtoWithId;
    }

    public async Task<ApplicationDto> UpdateAsync(
        Guid id,
        ApplicationNoIdDto applicationDto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(applicationDto);

        var application = await GetById(id, cancellationToken);

        ApplicationUpdater.UpdateStatus(application, applicationDto);

        var result = Converter.ToDto(application);
        await applicationDtoValidator.ValidateAndThrowAsync(result, cancellationToken);

        await repository.UpdateAsync(application, cancellationToken);

        return result;
    }

    public async Task SubmitAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var application = await GetById(id, cancellationToken);

        await applicationDtoValidator.ValidateAsync(
            Converter.ToDto(application),
            options => options.IncludeAllRuleSets().ThrowOnFailures(),
            cancellationToken);
        ThrowIfSubmitted(application);

        application.SubmittedTime = DateTime.Now;

        await repository.UpdateAsync(application, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var application = await GetById(id, cancellationToken);
        ThrowIfSubmitted(application);

        await repository.DeleteAsync(application, cancellationToken);
    }

    private static void ThrowIfSubmitted(Models.Application application)
    {
        if (application.SubmittedTime is not null)
        {
            throw WithHttpCodeException.NewBadRequest("Application is submitted");
        }
    }

    private async Task<Models.Application> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await repository.FindByIdAsync(id, cancellationToken)
               ?? throw WithHttpCodeException.NewNotFound($"Application with id {id} not found");
    }
}