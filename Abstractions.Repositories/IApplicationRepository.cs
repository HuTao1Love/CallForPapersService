using Models;

namespace Repositories;

public interface IApplicationRepository
{
    Task CreateAsync(Application application, CancellationToken cancellationToken = default);
    Task<Application?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Application?> FindNotSubmittedApplicationAsync(Guid authorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Application>> GetSubmittedApplicationsAsync(DateTime createdAfter, CancellationToken cancellationToken = default);
    Task<IEnumerable<Application>> GetUnsubmittedApplicationsAsync(DateTime createdBefore, CancellationToken cancellationToken = default);
    Task UpdateAsync(Application application, CancellationToken cancellationToken = default);
    Task DeleteAsync(Application application, CancellationToken cancellationToken = default);
}