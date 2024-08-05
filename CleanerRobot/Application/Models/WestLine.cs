namespace CleanerRobot.Application.Models;

public class WestLine : Line
{
    public WestLine(Point start, Point end) : base(start, end, LineDirection.West)
    {
    }

    public override int TotalPoints() => Math.Abs(Start.X - End.X);

    public override HashSet<Point> GetIntersections(IReadOnlyDictionary<int, List<Line>> perpendicularLines)
    {
        var intersections = new HashSet<Point>();

        foreach (var (xAxis, lines) in perpendicularLines)
        {
            var isInTheSameXAxis = xAxis >= End.X && xAxis < Start.X;
            if (!isInTheSameXAxis)
            {
                continue;
            }

            foreach (var line in lines)
            {
                if (IsIntersectingInYAxis(line))
                {
                    intersections.Add(new Point(xAxis, Start.Y));
                }
            }
        }

        return intersections;
    }

    public override HashSet<Point> GetOverlaps(IReadOnlyDictionary<int, List<Line>> parallelLines)
    {
        var overlaps = new HashSet<Point>();

        if (!parallelLines.TryGetValue(Start.Y, out var horLines))
        {
            return overlaps;
        }

        foreach (var horLine in horLines)
        {
            if (IsOverlapping(horLine))
            {
                if (horLine.Direction == LineDirection.West)
                {
                    if (Start.X <= horLine.Start.X && End.X >= horLine.End.X)
                    {
                        AddOverlappingRange(overlaps, End.X, Start.X);
                    }
                    else if (Start.X >= horLine.Start.X && End.X <= horLine.End.X)
                    {
                        AddOverlappingRange(overlaps, horLine.End.X, horLine.Start.X);
                    }
                    else if (Start.X >= horLine.Start.X && End.X <= horLine.Start.X)
                    {
                        AddOverlappingRange(overlaps, End.X, horLine.Start.X);
                    }
                    else if (Start.X <= horLine.Start.X && Start.X >= horLine.End.X)
                    {
                        AddOverlappingRange(overlaps, horLine.End.X, Start.X);
                    }
                }
                else if (horLine.Direction == LineDirection.East)
                {
                    if (Start.X <= horLine.End.X && End.X >= horLine.Start.X)
                    {
                        AddOverlappingRange(overlaps, End.X, Start.X);
                    }
                    else if (Start.X >= horLine.End.X && End.X <= horLine.Start.X)
                    {
                        AddOverlappingRange(overlaps, horLine.Start.X, horLine.End.X);
                    }
                    else if (Start.X >= horLine.Start.X && End.X <= horLine.Start.X)
                    {
                        AddOverlappingRange(overlaps, horLine.Start.X, Start.X);
                    }
                    else if (Start.X >= horLine.End.X && End.X >= horLine.Start.X)
                    {
                        AddOverlappingRange(overlaps, End.X, horLine.End.X);
                    }
                }
            }
        }

        return overlaps;
    }

    private bool IsIntersectingInYAxis(Line perpendicularLine)
    {
        if (perpendicularLine.Direction == LineDirection.North)
        {
            return perpendicularLine.Start.Y <= Start.Y && perpendicularLine.End.Y > Start.Y;
        }

        if (perpendicularLine.Direction == LineDirection.South)
        {
            return perpendicularLine.End.Y <= Start.Y && perpendicularLine.Start.Y > Start.Y;
        }

        return false;
    }


    private bool IsOverlapping(Line parallelLine)
    {
        if (parallelLine.Direction == LineDirection.West)
        {
            return (Start.X <= parallelLine.Start.X && End.X >= parallelLine.End.X) ||
                   (parallelLine.Start.X <= End.X && parallelLine.End.X >= Start.X);
        }

        if (parallelLine.Direction == LineDirection.East)
        {
            return (Start.X >= parallelLine.Start.X && End.X <= parallelLine.End.X) ||
                   (parallelLine.Start.X >= End.X && parallelLine.End.X <= Start.X);
        }

        return false;
    }

    private void AddOverlappingRange(HashSet<Point> overlaps, int start, int end)
    {
        if (start < end)
        {
            for (var x = start; x < end; x++)
            {
                overlaps.Add(new Point(x, Start.Y));
            }
        }
        else
        {
            for (var x = start; x > end; x--)
            {
                overlaps.Add(new Point(x, Start.Y));
            }
        }
    }
}