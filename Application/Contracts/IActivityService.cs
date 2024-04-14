namespace Application.Contracts;

public interface IActivityService
{
    Task<IEnumerable<ActivityDto>> GetActivityTypesAsync(CancellationToken cancellationToken = default);
}