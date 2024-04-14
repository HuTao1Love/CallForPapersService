namespace Application.Contracts;

public interface IApplicationService
{
    Task<IEnumerable<ApplicationDto>> GetApplicationsAsync(
        string? submittedAfterString,
        string? unsubmittedOlderString,
        CancellationToken cancellationToken = default);
    Task<ApplicationDto?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApplicationDto?> FindNotSubmittedApplicationAsync(Guid authorId, CancellationToken cancellationToken = default);
    Task<ApplicationDto> CreateAsync(ApplicationNoIdDto applicationDto, CancellationToken cancellationToken = default);
    Task<ApplicationDto> UpdateAsync(
        Guid id,
        ApplicationNoIdDto applicationDto,
        CancellationToken cancellationToken = default);
    Task SubmitAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}