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

        base.OnModelCreating(modelBuilder);
    }
}