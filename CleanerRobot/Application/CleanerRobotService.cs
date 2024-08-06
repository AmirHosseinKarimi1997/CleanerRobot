using System.Diagnostics;
using CleanerRobot.Application.Models;
using CleanerRobot.Data;
using CleanerRobot.Entity;

namespace CleanerRobot.Application;

public class CleanerRobotService : ICleanerRobotService
{
    private readonly ICleaningResultRepository _cleaningResultRepository;

    public CleanerRobotService(ICleaningResultRepository cleaningResultRepository)
    {
        _cleaningResultRepository = cleaningResultRepository;
    }

    public CleaningResult CleanOffice(CleaningRequest request)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var cleanedLines = new CleanedLines();
        long allUniqueSpacesCleanedCount = 1;
        
        var current = request.StartingPoint;
        foreach (var command in request.Commands)
        {
            var cleanedLine = CleanLine(current, command.Direction, command.Steps);
            current = cleanedLine.End;

            allUniqueSpacesCleanedCount += GetUniqueSpacesCleanedCount(cleanedLine, cleanedLines);
            cleanedLines.Add(cleanedLine);
        }

        stopwatch.Stop();
        var duration = stopwatch.Elapsed.TotalSeconds;

        var cleaningResult = new CleaningResult(DateTime.UtcNow, request.Commands.Count, allUniqueSpacesCleanedCount, duration);
        _cleaningResultRepository.Add(cleaningResult);
        
        return cleaningResult;
    }
    
    private static Line CleanLine(Point startPoint, string direction, int steps)
    {
        var x = startPoint.X;
        var y = startPoint.Y;
    
        switch (direction.ToLower())
        {
            case "north":
                y += steps;
                return new NorthLine(startPoint, new Point(x, y));
            case "east":
                x += steps;
                return new EastLine(startPoint, new Point(x, y));
            case "south":
                y -= steps;
                return new SouthLine(startPoint, new Point(x, y));
            case "west":
                x -= steps;
                return new WestLine(startPoint, new Point(x, y));
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static int GetUniqueSpacesCleanedCount(Line newlyCleanedLine, CleanedLines cleanedLines)
    {
        var perpendicularLines = newlyCleanedLine.IsHorizontal() ? cleanedLines.VerticalLines : cleanedLines.HorizontalLines;
        var moreThanOnceCleanedPoints = newlyCleanedLine.GetIntersections(perpendicularLines);

        var parallelLines = newlyCleanedLine.IsHorizontal() ? cleanedLines.HorizontalLines : cleanedLines.VerticalLines;
        moreThanOnceCleanedPoints.UnionWith(newlyCleanedLine.GetOverlaps(parallelLines));
        
        return newlyCleanedLine.TotalPoints() - moreThanOnceCleanedPoints.Count;
    }
}