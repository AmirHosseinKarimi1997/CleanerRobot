namespace CleanerRobot.Application.Models;

public class SouthLine : Line
{
    public SouthLine(Point start, Point end) : base(start, end, LineDirection.South)
    {
        if (start.X != end.X || start.Y <= end.Y)
        {
            throw new ArgumentException("South line is not valid");
        }
    }

    public override int TotalPoints() => Math.Abs(Start.Y - End.Y);

    public override HashSet<Point> GetIntersections(IReadOnlyDictionary<int, List<Line>> perpendicularLines)
    {
        var intersections = new HashSet<Point>();

        foreach (var (yAxis, lines) in perpendicularLines)
        {
            var isInTheSameYAxis = yAxis >= End.Y && yAxis < Start.Y;
            if (!isInTheSameYAxis)
            {
                continue;
            }

            foreach (var line in lines)
            {
                if (IsIntersectingInXAxis(line))
                {
                    intersections.Add(new Point(Start.X, yAxis));
                }
            }
        }

        return intersections;
    }

    public override HashSet<Point> GetOverlaps(IReadOnlyDictionary<int, List<Line>> parallelLines)
    {
        var overlaps = new HashSet<Point>();

        if (!parallelLines.TryGetValue(Start.X, out var vertLines))
        {
            return overlaps;
        }

        foreach (var vertLine in vertLines)
        {
            if (IsOverlapping(vertLine))
            {
                if (vertLine.Direction == LineDirection.South)
                {
                    if (Start.Y >= vertLine.Start.Y && End.Y <= vertLine.End.Y)
                    {
                        AddOverlappingRange(overlaps, Start.Y, End.Y);
                    }
                    else if (Start.Y <= vertLine.Start.Y && Start.Y >= vertLine.End.Y)
                    {
                        AddOverlappingRange(overlaps, Start.Y, vertLine.End.Y);
                    }
                    else if (Start.Y <= vertLine.Start.Y && End.Y >= vertLine.Start.Y)
                    {
                        AddOverlappingRange(overlaps, vertLine.Start.Y, End.Y);
                    }
                    else if (Start.Y >= vertLine.Start.Y && End.Y >= vertLine.End.Y)
                    {
                        AddOverlappingRange(overlaps, Start.Y, vertLine.End.Y);
                    }
                }
                else if (vertLine.Direction == LineDirection.North)
                {
                    if (Start.Y <= vertLine.End.Y && End.Y >= vertLine.End.Y)
                    {
                        AddOverlappingRange(overlaps, vertLine.End.Y, End.Y);
                    }
                    else if (Start.Y >= vertLine.End.Y && End.Y <= vertLine.Start.Y)
                    {
                        AddOverlappingRange(overlaps, End.Y, Start.Y);
                    }
                    else if (Start.Y >= vertLine.End.Y && End.Y >= vertLine.Start.Y)
                    {
                        AddOverlappingRange(overlaps, End.Y, vertLine.End.Y);
                    }
                    else if (Start.Y <= vertLine.Start.Y && End.Y >= vertLine.Start.Y)
                    {
                        AddOverlappingRange(overlaps, vertLine.End.Y, End.Y);
                    }
                }
            }
        }

        return overlaps;
    }

    private bool IsOverlapping(Line parallelLine)
    {
        if (parallelLine.Direction == LineDirection.North)
        {
            return (Start.Y >= parallelLine.End.Y && End.Y <= parallelLine.End.Y) ||
                   (parallelLine.Start.Y >= End.Y && parallelLine.End.Y <= Start.Y);
        }

        if (parallelLine.Direction == LineDirection.South)
        {
            return (Start.Y <= parallelLine.Start.Y && Start.Y >= parallelLine.End.Y) ||
                   (parallelLine.Start.Y <= End.Y && parallelLine.End.Y >= Start.Y);
        }

        return false;
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

    private void AddOverlappingRange(HashSet<Point> overlaps, int start, int end)
    {
        if (start < end)
        {
            for (var y = start; y < end; y++)
            {
                overlaps.Add(new Point(Start.X, y));
            }
        }
        else
        {
            for (var y = start; y > end; y--)
            {
                overlaps.Add(new Point(Start.X, y));
            }
        }
    }
}