using System.Globalization;
using Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace CallForPapersService.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationsController(IApplicationService applicationService) : ControllerBase
{
    private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.ff";
    private CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<ApplicationDto>> CreateApplication([FromBody] ApplicationDto applicationDto)
    {
        return NotFound();
    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<ApplicationDto>> EditApplication(Guid id, [FromBody] ApplicationDto applicationDto)
    {
        return NotFound();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> DeleteApplication(Guid id)
    {
        return NotFound();
    }

    [HttpPost("{id:Guid}/submit")]
    public async Task<ActionResult> SubmitApplication(Guid id)
    {
        return NotFound();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApplicationDto>> GetApplicationById(Guid id)
    {
        return NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetApplications(
        [FromQuery(Name = "submittedAfter")] string? submittedAfterString,
        [FromQuery(Name = "unsubmittedOlder")] string? unsubmittedOlderString)
    {
        if (submittedAfterString is null && unsubmittedOlderString is null)
        {
            return BadRequest("SubmittedAfter xor UnsubmittedOlder must be set");
        }

        if (submittedAfterString is not null && unsubmittedOlderString is not null)
        {
            return BadRequest("Only one of params SubmittedAfter and UnsubmittedOlder must be set");
        }

        if (!DateTime.TryParseExact(
                submittedAfterString ?? unsubmittedOlderString,
                DateTimeFormat,
                null,
                DateTimeStyles.None,
                out var dateTime))
        {
            return BadRequest($"Invalid datetime string format. Must be {DateTimeFormat}");
        }

        if (submittedAfterString is not null)
        {
            return new ActionResult<IEnumerable<ApplicationDto>>(
                await applicationService.GetSubmittedApplicationsAsync(dateTime, CancellationToken));
        }

        return new ActionResult<IEnumerable<ApplicationDto>>(
            await applicationService.GetUnsubmittedApplicationsAsync(dateTime, CancellationToken));
    }
}