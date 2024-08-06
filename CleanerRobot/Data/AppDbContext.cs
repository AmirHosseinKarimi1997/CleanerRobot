using CleanerRobot.Entity;
using Microsoft.EntityFrameworkCore;

namespace CleanerRobot.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<CleaningResult> CleaningResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CleaningResult>().HasKey(cr => cr.Id);
        modelBuilder.Entity<CleaningResult>().ToTable("cleaningresult");
    }
}