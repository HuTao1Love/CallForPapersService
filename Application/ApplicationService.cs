using Abstractions.Services;
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

    public async Task<ApplicationDto> CreateAsync(ApplicationDto applicationDto, CancellationToken cancellationToken = default)
    {
        await repository.CreateAsync(cancellationToken);
    }

    public async Task UpdateAsync(ApplicationDto application, CancellationToken cancellationToken = default)
    {
        await repository.UpdateAsync(cancellationToken);
    }

    public async Task DeleteAsync(ApplicationDto application, CancellationToken cancellationToken = default)
    {
        await repository.DeleteAsync(cancellationToken);
    }
}