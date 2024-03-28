using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Abstractions.Services;

public interface IApplicationService
{
    Task<ApplicationDto?> GetCurrentApplicationAsync(Guid authorId, CancellationToken cancellationToken = default);
    Task<ApplicationDto?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApplicationDto?> FindNotSubmittedApplicationAsync(Guid authorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationDto>> GetSubmittedApplicationsAsync(DateTime createdAfter, CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationDto>> GetUnsubmittedApplicationsAsync(DateTime createdBefore, CancellationToken cancellationToken = default);
    Task<ApplicationDto> CreateAsync(ApplicationDto applicationDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(ApplicationDto application, CancellationToken cancellationToken = default);
    Task DeleteAsync(ApplicationDto application, CancellationToken cancellationToken = default);
}