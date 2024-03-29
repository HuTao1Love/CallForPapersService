using Abstractions.Services;
using Application;
using Models;

namespace Test;

public class ActivityServiceTests
{
    private readonly IActivityService _activityService = new ActivityService();

    [Fact]
    public async Task AllValuesReturnedTest()
    {
        // ARRANGE
        var activitiesExpected = Enum.GetValues<Activity>().Select(i => i.ToString());

        // ACT
        var activities = await _activityService.GetActivityTypesAsync();

        // ARRANGE
        Assert.Equal(activitiesExpected, activities.Select(i => i.Activity));
    }
}