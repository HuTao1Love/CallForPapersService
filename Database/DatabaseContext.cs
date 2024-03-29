using Microsoft.EntityFrameworkCore;
using Models;

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

    public DbSet<Application> Applications { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.Entity<Application>()
            .Property(i => i.Name)
            .HasMaxLength(100);

        modelBuilder.Entity<Application>()
            .Property(i => i.Description)
            .HasMaxLength(300);

        modelBuilder.Entity<Application>()
            .Property(i => i.Outline)
            .HasMaxLength(1000);

        modelBuilder.Entity<Application>()
            .Property(i => i.Activity)
            .HasConversion<int>();

        modelBuilder.Entity<Application>()
            .Property(p => p.CreatedTime)
            .HasConversion(
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc));

        modelBuilder.Entity<Application>()
            .Property(p => p.SubmittedTime)
            .HasConversion(src => SetDateTimeKind(src), dst => SetDateTimeKind(dst));

        base.OnModelCreating(modelBuilder);
    }

    private static DateTime? SetDateTimeKind(DateTime? dateTime)
    {
        if (dateTime is null) return null;
        return dateTime.Value.Kind == DateTimeKind.Utc
            ? dateTime.Value
            : DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc);
    }
}