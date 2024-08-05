namespace CleanerRobot.Application.Models;

public class CleaningRequest
{
    public CleaningRequest(Position startingPosition, List<Command> commands)
    {
        StartingPosition = startingPosition;
        Commands = commands;
    }
    
    public Position StartingPosition { get; }
    public List<Command> Commands { get; }
}
