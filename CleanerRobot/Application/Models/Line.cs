namespace CleanerRobot.Application.Models;

public abstract class Line
{
    public Line(Point start, Point end, LineDirection direction)
    {
        Start = start;
        End = end;
        Direction = direction;
    }
    
    public Point Start { get; }
    public Point End { get; }
    public LineDirection Direction { get; }
    
    public bool IsHorizontal()
    {
        return this is EastLine or WestLine;
    }

    public bool IsVertical()
    {
        return this is NorthLine or SouthLine;
    }

    public abstract int TotalPoints();

    public abstract HashSet<Point> GetIntersections(IReadOnlyDictionary<int, List<Line>> perpendicularLines);
    public abstract HashSet<Point> GetOverlaps(IReadOnlyDictionary<int, List<Line>> parallelLines);
}