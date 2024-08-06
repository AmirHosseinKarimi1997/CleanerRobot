using CleanerRobot.Application.Models;
using FluentAssertions;

namespace Tests.UnitTests.Application.ModelTests;

public class CleanedLinesTests
{
    [Fact]
    public void AddHorizontalLineToPath()
    {
        // Arrange
        var path = new CleanedLines();
        
        const int y0 = 0;
        const int y5 = 5;
        
        var eastLine = new EastLine(new(0, y0), new(10, y0));
        var westLine = new WestLine(new(0, y5), new(-10, y5));

        // Act
        path.Add(eastLine);
        path.Add(westLine);

        // Assert
        path.VerticalLines.Should().BeEmpty();
        path.HorizontalLines.Should().HaveCount(2);
        path.HorizontalLines[y0].Should().HaveCount(1);
        path.HorizontalLines[y5].Should().HaveCount(1);
    }
    
    [Fact]
    public void AddVerticalLineToPath()
    {
        var path = new CleanedLines();
        
        const int x0 = 0;
        const int x5 = 5;
        
        var northLine = new NorthLine(new(x0, 0), new(x0, 10));
        var southLine = new SouthLine(new(x5, 0), new(x5, -10));

        // Act
        path.Add(northLine);
        path.Add(southLine);

        // Assert
        path.HorizontalLines.Should().BeEmpty();
        path.VerticalLines.Should().HaveCount(2);
        path.VerticalLines[x0].Should().HaveCount(1);
        path.VerticalLines[x5].Should().HaveCount(1);
    }
    
    [Fact]
    public void AddWithSameYAxis()
    {
        var path = new CleanedLines();
        
        const int y0 = 0;
        
        var eastLine = new EastLine(new(0, y0), new(10, y0));
        var westLine = new WestLine(new(0, y0), new(-10, y0));

        // Act
        path.Add(eastLine);
        path.Add(westLine);

        // Assert
        path.HorizontalLines.Should().HaveCount(1);
        path.HorizontalLines[y0].Should().HaveCount(2);
    }
    
    [Fact]
    public void AddWithSameXAxis()
    {
        var path = new CleanedLines();
        
        const int x0 = 0;
        
        var northLine = new NorthLine(new(x0, 0), new(x0, 10));
        var southLine = new SouthLine(new(x0, 0), new(x0, -10));

        // Act
        path.Add(northLine);
        path.Add(southLine);

        // Assert
        path.VerticalLines.Should().HaveCount(1);
        path.VerticalLines[x0].Should().HaveCount(2);
    }
}