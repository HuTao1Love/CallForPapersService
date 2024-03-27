using Models;
using Repositories;

namespace Database;

public class ActivityRepository : IActivityRepository
{
    public async Task<Activity?> FindByActivityId(Guid activityId)
    {
        throw new NotImplementedException();
    }

    public async Task<Activity?> FindNotSubmittedActivity(Guid authorId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Activity>> GetSubmittedUserActivities(Guid authorId)
    {
        throw new NotImplementedException();
    }

    public async Task<Activity> Create(
        Guid activityId,
        Guid authorId,
        ActivityType? activityType = null,
        string? name = null,
        string? description = null,
        string? outline = null)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Activity activity)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(Activity activity)
    {
        throw new NotImplementedException();
    }
}