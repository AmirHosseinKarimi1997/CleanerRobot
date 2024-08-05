using System.Diagnostics;
using CleanerRobot.Application.Models;
using CleanerRobot.Data;
using CleanerRobot.Entity;

namespace CleanerRobot.Application;

public class CleanerRobotService : ICleanerRobotService
{
    private readonly CleaningResultRepository _cleaningResultRepository;
    private readonly ICalculateOverlapService _calculateOverlapService;

    public CleanerRobotService(CleaningResultRepository cleaningResultRepository, ICalculateOverlapService calculateOverlapService)
    {
        _cleaningResultRepository = cleaningResultRepository;
        _calculateOverlapService = calculateOverlapService;
    }

    public CleaningResult CommandCleanOffice(CleaningRequest request)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var placesCleaned = CleanOffice(request.StartingPosition, request.Commands);

        stopwatch.Stop();
        double duration = stopwatch.Elapsed.TotalSeconds;

        var cleaningResult = new CleaningResult(DateTime.UtcNow, placesCleaned, request.Commands.Count, duration);
        
        _cleaningResultRepository.Add(cleaningResult);
        return cleaningResult;
    }

    private int CleanOffice(Position start, List<Command> commands)
    {
        var currentPosition = start;
        var allCleanedLines = new List<Line>();
        int allUniqueSpacesCleanedCount = 0;

        foreach (var command in commands)
        {
            var cleaningLineStartPosition = currentPosition;
            var cleanedLine = CleanLine(cleaningLineStartPosition, command.Direction, command.Steps);
            currentPosition = cleanedLine.EndPosition;

            var uniqueSpacesCleanedCount = GetUniqueSpacesCleanedCount(cleanedLine, allCleanedLines);
            allUniqueSpacesCleanedCount += uniqueSpacesCleanedCount;

            allCleanedLines.Add(cleanedLine);
        }

        return allUniqueSpacesCleanedCount;
    }
    
    private static Line CleanLine(Position startPosition, string direction, int steps)
    {
        LineDirection lineDirection;
        var x = startPosition.X;
        var y = startPosition.Y;
    
        switch (direction.ToLower())
        {
            case "north":
                y += steps;
                lineDirection = LineDirection.Vertical;
                break;
            case "east":
                x += steps;
                lineDirection = LineDirection.Horizontal;
                break;
            case "south":
                y -= steps;
                lineDirection = LineDirection.Vertical;
                break;
            case "west":
                x -= steps;
                lineDirection = LineDirection.Horizontal;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        var endPosition = new Position(x, y);
        return new Line(startPosition, endPosition, lineDirection);
    }

    private int GetUniqueSpacesCleanedCount(Line newlyCleanedLine, List<Line> allCleanedLines)
    {
        var newCleanedSpacesCount = newlyCleanedLine.SpacesCleanedCount();
        // If there are at least on cleaned line, we need to subtract one from the new cleaned spaces count to avoid counting the overlapping space twice
        newCleanedSpacesCount = allCleanedLines.Count == 0 ? newCleanedSpacesCount : newCleanedSpacesCount - 1;
        var overlapsWithPreviousCleanings = 0;

        foreach (var oldCleanedLine in allCleanedLines)
        {
            overlapsWithPreviousCleanings += GetOverlapsCount(newlyCleanedLine, oldCleanedLine);

            if (overlapsWithPreviousCleanings >= newCleanedSpacesCount)
            {
                return 0;
            }
        }

        var uniqueCleanedCount = newCleanedSpacesCount - overlapsWithPreviousCleanings;
        return uniqueCleanedCount > 0 ? uniqueCleanedCount : 0;
    }

    private int GetOverlapsCount(Line newCleanedLine, Line oldCleanedLine)
    {
        if (newCleanedLine.HasHorizontalOverlap(oldCleanedLine))
        {
            return _calculateOverlapService.CalculateHorizontalOverlap(newCleanedLine, oldCleanedLine);
        }

        if (newCleanedLine.HasVerticalOverlap(oldCleanedLine))
        {
            return _calculateOverlapService.CalculateVerticalOverlap(newCleanedLine, oldCleanedLine);
        }

        if (newCleanedLine.HasCrossingOverlap(oldCleanedLine))
        {
            return 1;
        }

        return 0;
    }
}