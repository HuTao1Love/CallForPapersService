using Application;
using Application.Contracts;
using Models;

namespace UnitTests;

public class ActivityServiceTests
{
    private readonly IActivityService activityService = new ActivityService();

    [Fact]
    public async Task AllValuesReturnedTest()
    {
        // ARRANGE
        var activitiesExpected = Enum.GetValues<Activity>().Select(i => i.ToString());

        // ACT
        var activities = await activityService.GetActivityTypesAsync();

        // ARRANGE
        Assert.Equal(activitiesExpected, activities.Select(i => i.Activity));
    }
}