using Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection LoadApplication(this IServiceCollection collection)
        => collection
            .AddScoped<IApplicationService, ApplicationService>()
            .AddScoped<IActivityService, ActivityService>();
}