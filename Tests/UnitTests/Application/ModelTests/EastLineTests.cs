using CleanerRobot.Application.Models;
using FluentAssertions;

namespace Tests.UnitTests.Application.ModelTests;

public class EastLineTests
{
    [Theory]
    [InlineData(-10, 0, 10)]
    [InlineData(10, 20, 10)]
    public void GetTotalPoints_ShouldReturnCorrectValue(int startx, int endx, int expected)
    {
        // Arrange
        var eastLine = new EastLine(new(startx, 0), new(endx, 0));
        
        // Act
        var totalPoints = eastLine.TotalPoints();
        
        // Assert
        totalPoints.Should().Be(expected);
    }
    
    [Fact]
    public void InvalidEastLine_ShouldThrowException()
    {
        // Arrange & Act
        Action act = () => new EastLine(new(0, 0), new(-10, 0));
        Action act2 = () => new EastLine(new(0, 0), new(10, 4));
        
        // Assert
        act.Should().Throw<ArgumentException>();
        act2.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void GetIntersectionsWithTwoLines_ShouldReturnCorrectValue()
    {
        // Arrange
        var northLine = new NorthLine(new(5, -5), new(5, 10));
        var southLine = new SouthLine(new(7, 3), new(7, -4));
        var cleanedPath = new CleanedPath();
        cleanedPath.Add(northLine);
        cleanedPath.Add(southLine);
        
        var eastLine = new EastLine(new(0, 0), new(10, 0));
        
        // Act
        var intersections = eastLine.GetIntersections(cleanedPath.VerticalLines);
        
        // Assert
        intersections.Count.Should().Be(2);
        intersections.Should().Contain(new Point(5, 0));
        intersections.Should().Contain(new Point(7, 0));
    }
    
    /// <summary>
    /// --------       ----------
    ///      -----------
    /// </summary>
    [Fact]
    public void GetOverlapWithTwoLines_ShouldReturnCorrectValue()
    {
        // Arrange
        var eastLine = new EastLine(new(2, 1), new(7, 1));
        var westLine = new WestLine(new(-2, 1), new(-7, 1));
        var cleanedPath = new CleanedPath();
        cleanedPath.Add(eastLine);
        cleanedPath.Add(westLine);
        
        var line = new EastLine(new(-5, 1), new(5, 1));
        
        // Act
        var overlap = line.GetOverlaps(cleanedPath.HorizontalLines);
        
        // Assert
        overlap.Count.Should().Be(6);
    }
}