using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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