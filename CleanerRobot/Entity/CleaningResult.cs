namespace CleanerRobot.Entity;

public class CleaningResult
{
    public CleaningResult()
    {
    }
    public CleaningResult(DateTime timestamp, int commands, long result, double duration)
    {
        Timestamp = timestamp;
        Result = result;
        Commands = commands;
        Duration = duration;
    }
    
    public int Id { get; private set; }
    public DateTime Timestamp { get; private set; }
    public long Result { get; private set; }
    public int Commands { get; private set; }
    public double Duration { get; private set; }
}