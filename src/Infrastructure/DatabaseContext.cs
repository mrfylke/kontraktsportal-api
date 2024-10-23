using Application;
using Domain.Deviations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public class DatabaseContext(IOptions<InfrastructureConfig> config) : DbContext, IUnitOfWork
{
    public DbSet<Deviation> Deviations { get; set; } = null!;
    public DbSet<DeviationType> DeviationTypes { get; set; } = null!;
    public DbSet<DeviationCategory> DeviationCategories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Deviation>().HasKey(d => d.Id);
        modelBuilder.Entity<DeviationType>().HasKey(d => d.Id);
        modelBuilder.Entity<DeviationCategory>().HasKey(d => d.Id);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(config.Value.ConnectionString);
    }
}