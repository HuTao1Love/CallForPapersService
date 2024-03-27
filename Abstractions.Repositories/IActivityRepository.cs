using Models;

namespace Repositories;

public interface IActivityRepository
{
    Task<Activity?> FindByActivityId(Guid activityId);
    Task<Activity?> FindNotSubmittedActivity(Guid authorId);
    Task<IEnumerable<Activity>> GetSubmittedUserActivities(Guid authorId);

    Task<Activity> Create(Guid activityId, Guid authorId, ActivityType? activityType = null, string? name = null, string? description = null, string? outline = null);
    Task Update(Activity activity);
    Task Delete(Activity activity);
}