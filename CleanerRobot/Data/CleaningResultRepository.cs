using CleanerRobot.Entity;

namespace CleanerRobot.Data;

public class CleaningResultRepository : ICleaningResultRepository
{
    private readonly AppDbContext _dbContext;

    public CleaningResultRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(CleaningResult cleaningResult)
    {
        _dbContext.CleaningResults.Add(cleaningResult);
        _dbContext.SaveChanges();
    }
}