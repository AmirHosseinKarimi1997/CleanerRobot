using CleanerRobot.Entity;

namespace CleanerRobot.Data;

public class PostgresDbHandler : IDbHandler
{
    private readonly AppDbContext _dbContext;

    public PostgresDbHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void InsertCleaningResult(CleaningResult cleaningResult)
    {
        _dbContext.CleaningResults.Add(cleaningResult);
        _dbContext.SaveChanges();
    }
}