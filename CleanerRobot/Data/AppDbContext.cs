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
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CleaningResult>().ToTable("cleaningresults");
        modelBuilder.Entity<CleaningResult>().HasKey(cr => cr.Id);
        
        modelBuilder.Entity<CleaningResult>().Property(cr => cr.Id).HasColumnName("id");
        modelBuilder.Entity<CleaningResult>().Property(cr => cr.Result).HasColumnName("result");
        modelBuilder.Entity<CleaningResult>().Property(cr => cr.Commands).HasColumnName("commands");
        modelBuilder.Entity<CleaningResult>().Property(cr => cr.Timestamp).HasColumnName("timestamp");
        modelBuilder.Entity<CleaningResult>().Property(cr => cr.Duration).HasColumnName("duration");
        
    }
}