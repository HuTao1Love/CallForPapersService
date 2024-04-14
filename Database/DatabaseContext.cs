using Microsoft.EntityFrameworkCore;

namespace Database;
public sealed class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
        Database.EnsureCreated();
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Models.Application> Applications { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.Entity<Models.Application>()
            .Property(i => i.Name)
            .HasMaxLength(100);

        modelBuilder.Entity<Models.Application>()
            .Property(i => i.Description)
            .HasMaxLength(300);

        modelBuilder.Entity<Models.Application>()
            .Property(i => i.Outline)
            .HasMaxLength(1000);

        modelBuilder.Entity<Models.Application>()
            .Property(i => i.Activity)
            .HasConversion<int>();

        modelBuilder.Entity<Models.Application>()
            .Property(p => p.CreatedTime)
            .HasConversion(
                src => SetDateTimeKind(src),
                dst => SetDateTimeKind(dst));

        modelBuilder.Entity<Models.Application>()
            .Property(p => p.SubmittedTime)
            .HasConversion(
                src => SetDateTimeKind(src),
                dst => SetDateTimeKind(dst));

        base.OnModelCreating(modelBuilder);
    }

    private static DateTime SetDateTimeKind(DateTime dateTime)
    {
        return dateTime.Kind == DateTimeKind.Utc
            ? dateTime
            : DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }

    private static DateTime? SetDateTimeKind(DateTime? dateTime)
    {
        if (dateTime is null)
        {
            return null;
        }

        return dateTime.Value.Kind == DateTimeKind.Utc
            ? dateTime.Value
            : DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc);
    }
}