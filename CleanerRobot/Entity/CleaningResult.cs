using Microsoft.EntityFrameworkCore;

namespace CleanerRobot.Entity;

public class CleaningResult
{
    public CleaningResult(DateTime timestamp, int commands, long result, double duration)
    {
        Timestamp = timestamp;
        Result = result;
        Commands = commands;
        Duration = duration;
    }
    
    public int Id { get; private set; }
    public DateTime Timestamp { get; }
    public long Result { get; }
    public int Commands { get; }
    public double Duration { get; }
}