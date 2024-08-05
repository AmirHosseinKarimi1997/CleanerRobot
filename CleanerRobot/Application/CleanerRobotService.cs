using System.Diagnostics;
using CleanerRobot.Application.Models;
using CleanerRobot.Data;
using CleanerRobot.Entity;

namespace CleanerRobot.Application;

public class CleanerRobotService : ICleanerRobotService
{
    private readonly IDbHandler _dbHandler;

    public CleanerRobotService(IDbHandler dbHandler)
    {
        _dbHandler = dbHandler;
    }

    public CleaningResult CleanOffice(CleaningRequest request)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var cleanedPath = new CleanedPath();
        long allUniqueSpacesCleanedCount = 1;
        
        var current = request.StartingPoint;
        foreach (var command in request.Commands)
        {
            var cleanedLine = CleanLine(current, command.Direction, command.Steps);
            current = cleanedLine.End;

            allUniqueSpacesCleanedCount += GetUniqueSpacesCleanedCount(cleanedLine, cleanedPath);
            cleanedPath.Add(cleanedLine);
        }

        stopwatch.Stop();
        double duration = stopwatch.Elapsed.TotalSeconds;

        var cleaningResult = new CleaningResult(DateTime.UtcNow, request.Commands.Count, allUniqueSpacesCleanedCount, duration);
        _dbHandler.InsertCleaningResult(cleaningResult);
        
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

    private int GetUniqueSpacesCleanedCount(Line newlyCleanedLine, CleanedPath cleanedPath)
    {
        var perpendicularLines = newlyCleanedLine.IsHorizontal() ? cleanedPath.VerticalLines : cleanedPath.HorizontalLines;
        var intersectionsAndOverlaps = newlyCleanedLine.GetIntersections(perpendicularLines);

        var parallelLines = newlyCleanedLine.IsHorizontal() ? cleanedPath.HorizontalLines : cleanedPath.VerticalLines;
        intersectionsAndOverlaps.UnionWith(newlyCleanedLine.GetOverlaps(parallelLines));
        
        return newlyCleanedLine.TotalPoints() - intersectionsAndOverlaps.Count;
    }
}