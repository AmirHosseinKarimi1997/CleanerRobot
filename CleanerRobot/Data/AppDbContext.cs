using CleanerRobot.Entity;
using Microsoft.EntityFrameworkCore;

namespace CleanerRobot.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<CleaningResult> CleaningResults { get; set; }
}