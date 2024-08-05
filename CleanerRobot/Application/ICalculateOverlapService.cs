using CleanerRobot.Application.Models;

namespace CleanerRobot.Application;

public interface ICalculateOverlapService
{
    int CalculateVerticalOverlap(Line line, Line other);
    
    int CalculateHorizontalOverlap(Line line, Line other);
}