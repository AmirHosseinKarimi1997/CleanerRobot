using CleanerRobot.Application.Models;
using FluentAssertions;

namespace Tests.UnitTests.Application.ModelTests;

public class SouthLineTests
{
    [Theory]
    [InlineData(-10, -20, 10)]
    [InlineData(10, 0, 10)]
    public void GetTotalPoints_ShouldReturnCorrectValue(int startY, int endY, int expected)
    {
        // Arrange
        var southLine = new SouthLine(new(0, startY), new(0, endY));
        
        // Act
        var totalPoints = southLine.TotalPoints();
        
        // Assert
        totalPoints.Should().Be(expected);
    }
    
    [Fact]
    public void InvalidSouthLine_ShouldThrow()
    {
        // Arrange & Act
        Action act = () => new SouthLine(new(0, 0), new(0, 10));
        Action act2 = () => new SouthLine(new(0, 0), new(10, -4));
        
        // Assert
        act.Should().Throw<ArgumentException>();
        act2.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetIntersectionsWithTwoLines_ShouldReturnCorrectValue()
    {
        // Arrange
        var eastLine = new EastLine(new(-5, 2), new(5, 2));
        var westLine = new WestLine(new(7, 9), new(-2, 9));
        var cleanedLines = new CleanedLines();
        cleanedLines.Add(eastLine);
        cleanedLines.Add(westLine);
        
        var southLine = new SouthLine(new(1, 10), new(1, -10));
        
        // Act
        var intersections = southLine.GetIntersections(cleanedLines.HorizontalLines);
        
        // Assert
        intersections.Count.Should().Be(2);
        intersections.Should().Contain(new Point(1, 2));
        intersections.Should().Contain(new Point(1, 9));
    }
    
    [Fact]
    public void GetOverlapWithTwoLines_ShouldReturnCorrectValue()
    {
        // Arrange
        var northLine = new NorthLine(new(0, -7), new(0, -2));
        var southLine = new SouthLine(new(0, 7), new(0, 2));
        var cleanedLines = new CleanedLines();
        cleanedLines.Add(northLine);
        cleanedLines.Add(southLine);
        
        var line = new SouthLine(new(0, 5), new(0, -5));
        
        // Act
        var overlaps = line.GetOverlaps(cleanedLines.VerticalLines);
        
        // Assert
        overlaps.Count.Should().Be(6);
    }
}