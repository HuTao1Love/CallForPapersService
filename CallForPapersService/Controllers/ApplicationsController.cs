using System.Globalization;
using Abstractions.Services;
using CallForPapersService.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CallForPapersService.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationsController(IApplicationService applicationService) : ControllerBase
{
    private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.ff";
    private CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<ApplicationDto>> CreateApplication([FromBody] ApplicationNoIdDto applicationDto)
    {
        return await applicationService.CreateAsync(applicationDto);
    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<ApplicationDto>> EditApplication(Guid id, [FromBody] ApplicationNoIdDto applicationDto)
    {
        return await applicationService.UpdateAsync(id, applicationDto);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> DeleteApplication(Guid id)
    {
        await applicationService.DeleteAsync(id, CancellationToken);
        return Ok();
    }

    [HttpPost("{id:Guid}/submit")]
    public async Task<ActionResult> SubmitApplication(Guid id)
    {
        await applicationService.SubmitAsync(id, CancellationToken);
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApplicationDto>> GetApplicationById(Guid id)
    {
        var applicationDto = await applicationService.FindByIdAsync(id, CancellationToken);

        if (applicationDto is null) throw new NotFoundException($"Application with ID {id}");

        return applicationDto;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetApplications(
        [FromQuery(Name = "submittedAfter")] string? submittedAfterString,
        [FromQuery(Name = "unsubmittedOlder")] string? unsubmittedOlderString)
    {
        if (submittedAfterString is null && unsubmittedOlderString is null)
        {
            throw new BadRequestException("SubmittedAfter xor UnsubmittedOlder must be set");
        }

        if (submittedAfterString is not null && unsubmittedOlderString is not null)
        {
            throw new BadRequestException("Only one of params SubmittedAfter and UnsubmittedOlder must be set");
        }

        if (!DateTime.TryParseExact(
                submittedAfterString ?? unsubmittedOlderString,
                DateTimeFormat,
                null,
                DateTimeStyles.None,
                out var dateTime))
        {
            throw new BadRequestException($"Invalid datetime string format. Must be {DateTimeFormat}");
        }

        return new ActionResult<IEnumerable<ApplicationDto>>(
            submittedAfterString is not null
                ? await applicationService.GetSubmittedApplicationsAsync(dateTime, CancellationToken)
                : await applicationService.GetUnsubmittedApplicationsAsync(dateTime, CancellationToken));
    }
}