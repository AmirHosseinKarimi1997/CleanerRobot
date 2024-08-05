namespace CleanerRobot.Application.Models;

public class NorthLine : Line
{
    public NorthLine(Point start, Point end) : base(start, end, LineDirection.North)
    {
    }

    public override int TotalPoints() => Math.Abs(End.Y - Start.Y);

    public override HashSet<int> GetIntersections(IReadOnlyDictionary<int, List<Line>> perpendicularLines)
    {
        var intersections = new HashSet<int>();

        foreach (var (yAxis, lines) in perpendicularLines)
        {
            var isInTheSameYAxis = yAxis > Start.Y && yAxis <= End.Y;
            if (!isInTheSameYAxis)
            {
                continue;
            }
            
            foreach (var line in lines)
            {
                if (IsIntersectingInXAxis(line))
                {
                    intersections.Add(yAxis);
                }
            }
        }

        return intersections;
    }

    public override HashSet<int> GetOverlaps(IReadOnlyDictionary<int, List<Line>> parallelLines)
    {
        var overlaps = new HashSet<int>();

        if (!parallelLines.TryGetValue(Start.X, out var vertLines))
        {
            return overlaps;
        }

        foreach (var vertLine in vertLines)
        {
            if (IsOverlapping(vertLine))
            {
                if (vertLine.Direction == LineDirection.North)
                {
                    if (Start.Y <= vertLine.Start.Y && End.Y >= vertLine.End.Y)
                    {
                        AddOverlappingRange(overlaps, End.Y, Start.Y);
                    }
                    else if (Start.Y >= vertLine.Start.Y && End.Y <= vertLine.End.Y)
                    {
                        AddOverlappingRange(overlaps, vertLine.End.Y, vertLine.Start.Y);
                    }
                    else if (Start.Y >= vertLine.Start.Y && End.Y <= vertLine.Start.Y &&
                             End.Y >= vertLine.End.Y)
                    {
                        AddOverlappingRange(overlaps, vertLine.End.Y, End.Y);
                    }
                    else if (Start.Y <= vertLine.Start.Y && Start.Y >= vertLine.End.Y &&
                             End.Y <= vertLine.End.Y)
                    {
                        AddOverlappingRange(overlaps, Start.Y, vertLine.End.Y);
                    }
                }
                else if (vertLine.Direction == LineDirection.South)
                {
                    if (Start.Y >= vertLine.End.Y && End.Y <= vertLine.End.Y &&
                        End.Y >= vertLine.Start.Y)
                    {
                        AddOverlappingRange(overlaps, End.Y, vertLine.End.Y);
                    }
                    else if (Start.Y >= vertLine.Start.Y && Start.Y <= vertLine.End.Y &&
                             End.Y <= vertLine.Start.Y)
                    {
                        AddOverlappingRange(overlaps, vertLine.Start.Y, Start.Y);
                    }
                    else if (Start.Y <= vertLine.End.Y && End.Y >= vertLine.Start.Y)
                    {
                        AddOverlappingRange(overlaps, End.Y, Start.Y);
                    }
                    else if (Start.Y >= vertLine.End.Y && End.Y <= vertLine.Start.Y)
                    {
                        AddOverlappingRange(overlaps, vertLine.Start.Y, vertLine.End.Y);
                    }
                }
            }
        }

        return overlaps;
    }

    private bool IsOverlapping(Line parallelLine)
    {
        return (Start.Y <= parallelLine.End.Y && End.Y >= parallelLine.Start.Y) ||
               (parallelLine.Start.Y <= End.Y && parallelLine.End.Y >= Start.Y);
    }
    
    private bool IsIntersectingInXAxis(Line perpendicularLine)
    {
        if (perpendicularLine.Direction == LineDirection.East)
        {
            return perpendicularLine.Start.X <= Start.X && perpendicularLine.End.X > Start.X;
        }

        if (perpendicularLine.Direction == LineDirection.West)
        {
            return perpendicularLine.End.X <= Start.X && perpendicularLine.Start.X > Start.X;
        }

        return false;
    }
}