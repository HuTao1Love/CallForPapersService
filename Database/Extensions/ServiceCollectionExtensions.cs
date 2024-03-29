using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;

namespace Database.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection LoadDatabase(this IServiceCollection collection, IConfiguration configuration)
        => collection
            .AddDbContextFactory<DatabaseContext>(options => options.UseNpgsql(configuration["ConnectionString"]))
            .AddScoped<IApplicationRepository, ApplicationRepository>();
}