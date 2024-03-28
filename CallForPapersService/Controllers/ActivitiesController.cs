using Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace CallForPapersService.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivitiesController(IActivityService activityService) : ControllerBase
{
    private CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpGet]
    public async Task<IEnumerable<ActivityDto>> GetActivitiesAsync()
    {
        return await activityService.GetActivityTypesAsync(CancellationToken);
    }
}