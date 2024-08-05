using CleanerRobot.Application.Models;

namespace CleanerRobot.Application;

public static class LineExtensions
{
    public static bool HasHorizontalOverlap(this Line line, Line other)
    {
        if (!line.IsHorizontal() || !other.IsHorizontal())
        {
            return false;
        }

        if (line.StartPosition.Y == other.StartPosition.Y)
        {
            return Math.Max(line.StartPosition.X, line.EndPosition.X) >= Math.Min(other.StartPosition.X, other.EndPosition.X) &&
                Math.Max(other.StartPosition.X, other.EndPosition.X) >= Math.Min(line.StartPosition.X, line.EndPosition.X);
        }

        return false;
    }
    
    public static bool HasVerticalOverlap(this Line line, Line other)
    {
        if (!line.IsVertical() || !other.IsVertical())
        {
            return false;
        }

        if (line.StartPosition.X == other.StartPosition.X)
        {
            return Math.Max(line.StartPosition.Y, line.EndPosition.Y) >= Math.Min(other.StartPosition.Y, other.EndPosition.Y) &&
                Math.Max(other.StartPosition.Y, other.EndPosition.Y) >= Math.Min(line.StartPosition.Y, line.EndPosition.Y);
        }

        return false;
    }
    
    
    public static bool HasCrossingOverlap(this Line line, Line other)
    {
        Line horizontalLine;
        Line verticalLine;

        if (line.IsHorizontal() && other.IsVertical())
        {
            horizontalLine = line;
            verticalLine = other;
        }
        else if (line.IsVertical() && other.IsHorizontal())
        {
            horizontalLine = other;
            verticalLine = line;
        }
        else
        {
            return false;
        }

        return Math.Min(horizontalLine.StartPosition.X, horizontalLine.EndPosition.X) <= verticalLine.StartPosition.X &&
            Math.Max(horizontalLine.StartPosition.X, horizontalLine.EndPosition.X) >= verticalLine.StartPosition.X &&
            Math.Min(verticalLine.StartPosition.Y, verticalLine.EndPosition.Y) <= horizontalLine.StartPosition.Y &&
            Math.Max(verticalLine.StartPosition.Y, verticalLine.EndPosition.Y) >= horizontalLine.StartPosition.Y;
        
        // return verticalLine.StartPosition.X >= horizontalLine.StartPosition.X &&
        //        verticalLine.StartPosition.X <= horizontalLine.EndPosition.X &&
        //        horizontalLine.StartPosition.Y >= verticalLine.StartPosition.Y &&
        //        horizontalLine.StartPosition.Y <= verticalLine.EndPosition.Y;
    }
}