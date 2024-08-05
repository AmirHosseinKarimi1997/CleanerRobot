using CleanerRobot.Application.Models;

namespace CleanerRobot.Application;

public class CalculateOverlapService : ICalculateOverlapService
{
    public int CalculateVerticalOverlap(Line line, Line other)
    {
        int overlapStart = Math.Max(line.StartPosition.Y, other.StartPosition.Y);
        int overlapEnd = Math.Min(line.EndPosition.Y, other.EndPosition.Y);
        return Math.Abs(overlapEnd - overlapStart);
    }

    public int CalculateHorizontalOverlap(Line line, Line other)
    {
        int overlapStart = Math.Max(line.StartPosition.X, other.StartPosition.X);
        int overlapEnd = Math.Min(line.EndPosition.X, other.EndPosition.X);
        return Math.Abs(overlapEnd - overlapStart);
    }
}