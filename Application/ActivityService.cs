using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Services;
using Models;

namespace Application;

public class ActivityService : IActivityService
{
    public Task<IEnumerable<ActivityDto>> GetActivityTypesAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            Enum.GetValues<Activity>()
                .Select(i => new ActivityDto(Enum.GetName(i)!, i.GetDescription())));
    }
}