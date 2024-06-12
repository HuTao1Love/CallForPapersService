using Application.Contracts;
using Application.Services;
using Models;

namespace Application;

public class ActivityService : IActivityService
{
    public Task<IEnumerable<ActivityDto>> GetActivityTypesAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            Enum.GetValues<Activity>()
                .Select(i => new ActivityDto(Enum.GetName(i)!, ActivityDescriptionProvider.GetDescription(i))));
    }
}