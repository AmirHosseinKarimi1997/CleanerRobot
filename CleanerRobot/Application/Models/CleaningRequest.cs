namespace CleanerRobot.Application.Models;

public class CleaningRequest
{
    public CleaningRequest(Point startingPoint, List<Command> commands)
    {
        StartingPoint = startingPoint;
        Commands = commands;
    }
    
    public Point StartingPoint { get; }
    public List<Command> Commands { get; }
}
