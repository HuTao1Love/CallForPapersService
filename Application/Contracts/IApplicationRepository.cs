namespace Application.Contracts;

public interface IApplicationRepository
{
    Task<IEnumerable<Models.Application>> GetSubmittedApplicationsAsync(
        DateTime createdAfter,
        CancellationToken cancellationToken = default);
    Task<IEnumerable<Models.Application>> GetNotSubmittedApplicationsAsync(
        DateTime createdBefore,
        CancellationToken cancellationToken = default);
    Task<Models.Application?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Models.Application?> FindNotSubmittedApplicationAsync(
        Guid authorId,
        CancellationToken cancellationToken = default);
    Task CreateAsync(Models.Application application, CancellationToken cancellationToken = default);
    Task UpdateAsync(Models.Application application, CancellationToken cancellationToken = default);
    Task DeleteAsync(Models.Application application, CancellationToken cancellationToken = default);
}