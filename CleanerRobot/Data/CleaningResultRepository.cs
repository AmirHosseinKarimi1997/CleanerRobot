using CleanerRobot.Entity;

namespace CleanerRobot.Data;

public class CleaningResultRepository
{
    private readonly AppDbContext _context;

    public CleaningResultRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(CleaningResult cleaningResult)
    {
        _context.CleaningResults.Add(cleaningResult);
        _context.SaveChanges();
    }
}