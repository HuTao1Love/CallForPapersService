using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Abstractions.Services;

public interface IActivityService
{
    Task<IEnumerable<ActivityDto>> GetActivityTypesAsync(CancellationToken cancellationToken = default);
}