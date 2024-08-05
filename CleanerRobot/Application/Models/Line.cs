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

    public abstract HashSet<int> GetIntersections(IReadOnlyDictionary<int, List<Line>> perpendicularLines);
    public abstract HashSet<int> GetOverlaps(IReadOnlyDictionary<int, List<Line>> parallelLines);
    
    protected void AddRangeToSet(HashSet<int> set, int start, int end)
    {
        for (int i = start; i < end; i++)
        {
            set.Add(i);
        }
    }
    
    protected void AddOverlappingRange(HashSet<int> overlaps, int start, int end)
    {
        if (start < end)
        {
            AddRangeToSet(overlaps, start, end);
        }
        else
        {
            AddRangeToSet(overlaps, end, start);
        }
    }
}