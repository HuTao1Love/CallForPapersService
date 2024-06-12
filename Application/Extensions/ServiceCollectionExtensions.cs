using Application.Contracts;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection LoadApplication(this IServiceCollection collection)
        => collection
            .AddScoped<IApplicationService, ApplicationService>()
            .AddScoped<IActivityService, ActivityService>()
            .AddScoped<IValidator<ApplicationDto>, ApplicationDtoValidator>();
}