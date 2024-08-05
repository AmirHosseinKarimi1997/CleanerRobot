using Microsoft.EntityFrameworkCore;

namespace CleanerRobot.Entity;

public class CleaningResult
{
    public CleaningResult(DateTime requestedAt, long uniquePlacesCleaned, int commandCount, double duration)
    {
        RequestedAt = requestedAt;
        UniquePlacesCleaned = uniquePlacesCleaned;
        CommandCount = commandCount;
        Duration = duration;
    }
    
    public int Id { get; private set; }
    public DateTime RequestedAt { get; }
    public long UniquePlacesCleaned { get; }
    public int CommandCount { get; }
    public double Duration { get; }
}