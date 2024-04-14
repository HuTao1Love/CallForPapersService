using Application.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class ApplicationRepository(IDbContextFactory<DatabaseContext> contextFactory) : IApplicationRepository
{
    public async Task<Models.Application?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Applications.Where(i => i.Id == id).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<Models.Application?> FindNotSubmittedApplicationAsync(
        Guid authorId,
        CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Applications
            .Where(i => i.AuthorId == authorId && i.SubmittedTime == null)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Models.Application>> GetSubmittedApplicationsAsync(
        DateTime createdAfter,
        CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Applications
            .Where(i => i.SubmittedTime >= createdAfter)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Models.Application>> GetNotSubmittedApplicationsAsync(
        DateTime createdBefore,
        CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Applications
            .Where(i => i.CreatedTime <= createdBefore && i.SubmittedTime == null)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(
        Models.Application application,
        CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        await context.Applications.AddAsync(application, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Models.Application application, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        context.Applications.Update(application);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Models.Application application, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        context.Applications.Remove(application);
        await context.SaveChangesAsync(cancellationToken);
    }
}