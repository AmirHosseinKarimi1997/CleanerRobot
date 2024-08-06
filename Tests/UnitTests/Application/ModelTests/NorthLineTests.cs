using CleanerRobot.Application.Models;
using FluentAssertions;

namespace Tests.UnitTests.Application.ModelTests;

public class NorthLineTests
{
    [Theory]
    [InlineData(-10, 0, 10)]
    [InlineData(10, 20, 10)]
    public void GetTotalPoints_ShouldReturnCorrectValue(int startY, int endY, int expected)
    {
        // Arrange
        var northLine = new NorthLine(new(0, startY), new(0, endY));
        
        // Act
        var totalPoints = northLine.TotalPoints();
        
        // Assert
        totalPoints.Should().Be(expected);
    }
    
    [Fact]
    public void InvalidNorthLine_ShouldThrow()
    {
        // Arrange & Act
        Action act = () => new NorthLine(new(0, 0), new(0, -10));
        Action act2 = () => new NorthLine(new(0, 0), new(10, 4));
        
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
        var cleanedPath = new CleanedPath();
        cleanedPath.Add(eastLine);
        cleanedPath.Add(westLine);
        
        var northLine = new NorthLine(new(1, 0), new(1, 10));
        
        // Act
        var intersections = northLine.GetIntersections(cleanedPath.HorizontalLines);
        
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
        var cleanedPath = new CleanedPath();
        cleanedPath.Add(northLine);
        cleanedPath.Add(southLine);
        
        var line = new NorthLine(new(0, -5), new(0, 5));
        
        // Act
        var overlaps = line.GetOverlaps(cleanedPath.VerticalLines);
        
        // Assert
        overlaps.Count.Should().Be(6);
    }
}