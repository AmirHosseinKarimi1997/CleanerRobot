namespace CleanerRobot.Application.Models;

public class Line
{
    public Line(Position startPosition, Position endPosition, LineDirection lineDirection)
    {
        StartPosition = startPosition;
        EndPosition = endPosition;
        LineDirection = lineDirection;
    }
    
    public Position StartPosition { get; }
    public Position EndPosition { get; }
    public LineDirection LineDirection { get; }
    
    public bool IsHorizontal()
    {
        return LineDirection is LineDirection.Horizontal;
    }

    public bool IsVertical()
    {
        return LineDirection is LineDirection.Vertical;
    }

    public int SpacesCleanedCount()
    {
        if (LineDirection is LineDirection.Vertical)
        {
            return Math.Abs(EndPosition.Y - StartPosition.Y) + 1;
        }
        if (LineDirection is LineDirection.Horizontal)
        {
            return Math.Abs(EndPosition.X - StartPosition.X) + 1;
        }

        return 0;
    }
}