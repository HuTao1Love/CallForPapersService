using Abstractions.Services;
using Application.Exceptions;
using Models.Exceptions;
using Repositories;

namespace Application;

public class ApplicationService(IApplicationRepository repository) : IApplicationService
{
    public async Task<ApplicationDto?> GetCurrentApplicationAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        return (await repository.FindNotSubmittedApplicationAsync(authorId, cancellationToken))?.AsDto();
    }

    public async Task<ApplicationDto?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return (await repository.FindByIdAsync(id, cancellationToken))?.AsDto();
    }

    public async Task<ApplicationDto?> FindNotSubmittedApplicationAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        return (await repository.FindNotSubmittedApplicationAsync(authorId, cancellationToken))?.AsDto();
    }

    public async Task<IEnumerable<ApplicationDto>> GetSubmittedApplicationsAsync(DateTime createdAfter, CancellationToken cancellationToken = default)
    {
        return (await repository.GetSubmittedApplicationsAsync(createdAfter, cancellationToken)).Select(i => i.AsDto());
    }

    public async Task<IEnumerable<ApplicationDto>> GetUnsubmittedApplicationsAsync(DateTime createdBefore, CancellationToken cancellationToken = default)
    {
        return (await repository.GetUnsubmittedApplicationsAsync(createdBefore, cancellationToken)).Select(i => i.AsDto());
    }

    public async Task<ApplicationDto> CreateAsync(ApplicationNoIdDto applicationDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(applicationDto);

        if (applicationDto.Author is null) throw new AuthorMustBeDefinedException();

        if (await FindNotSubmittedApplicationAsync(applicationDto.Author.Value, cancellationToken) is not null)
            throw new UserAlreadyHaveUnsubmittedApplicationException();

        ApplicationDto applicationDtoWithId = applicationDto.WithId(Guid.NewGuid(), applicationDto.Author.Value);
        if (!applicationDtoWithId.AnyDefined()) throw new AnyFieldMustBeDefinedException();

        var application = applicationDtoWithId.AsEntity(DateTime.Now);
        await repository.CreateAsync(application, cancellationToken);
        return application.AsDto();
    }

    public async Task<ApplicationDto> UpdateAsync(Guid id, ApplicationNoIdDto applicationDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(applicationDto);

        var application = await repository.FindByIdAsync(id, cancellationToken);
        if (applicationDto.Author is not null) throw new InvalidUpdateException("Author cannot be changed");
        if (application is null) throw new NullException($"Application with id {id} not found");
        if (application.SubmittedTime is not null) throw new ApplicationIsSubmittedException();

        application.Activity = UpdateIfNotNull(application.Activity.ToString(), applicationDto.Activity).FromString();
        application.Name = UpdateIfNotNull(application.Name, applicationDto.Name);
        application.Description = UpdateIfNotNull(application.Description, applicationDto.Description);
        application.Outline = UpdateIfNotNull(application.Outline, applicationDto.Outline);

        var result = application.AsDto();

        if (!result.AnyDefined()) throw new AnyFieldMustBeDefinedException();
        await repository.UpdateAsync(application, cancellationToken);
        return result;
    }

    public async Task SubmitAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var application = await repository.FindByIdAsync(id, cancellationToken);
        if (application is null) throw new NullException($"Application with id {id} not found");

        if (!application.AsDto().AllRequiredDefined()) throw new NotDefinedException();
        if (application.SubmittedTime is not null) throw new ApplicationIsSubmittedException();

        application.SubmittedTime = DateTime.Now;

        await repository.UpdateAsync(application, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var application = await repository.FindByIdAsync(id, cancellationToken);

        if (application is null) throw new NullException($"Application with id {id} not found");
        if (application.SubmittedTime is not null) throw new ApplicationIsSubmittedException();

        await repository.DeleteAsync(application, cancellationToken);
    }

    private static string? UpdateIfNotNull(string? src, string? value)
    {
        if (value is null) return src;

        if (value.Length == 0 || value.Equals("null", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        return value;
    }
}