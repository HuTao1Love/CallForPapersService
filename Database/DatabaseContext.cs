using Microsoft.EntityFrameworkCore;
using Models;

namespace Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext() { }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { }

    public virtual DbSet<Activity> Activities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(builder => )
        base.OnModelCreating(modelBuilder);
    }
}