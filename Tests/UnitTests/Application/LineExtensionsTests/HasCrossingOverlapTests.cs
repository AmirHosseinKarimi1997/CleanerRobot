using CleanerRobot.Application;
using CleanerRobot.Application.Models;
using FluentAssertions;

namespace Tests.UnitTests.Application.LineExtensionsTests;

public class HasCrossingOverlapTests
{
    [Fact]
    public void WhenLine1IsCompletelyLeftOfLine2_ShouldReturnFalse()
    {
        // Arrange
        var line1 = new Line(new Position(0, 0), new Position(2, 0), LineDirection.Horizontal);
        var line2 = new Line(new Position(4, 0), new Position(4, 4), LineDirection.Vertical);

        // Act
        var result = line1.HasCrossingOverlap(line2);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WhenLine1IsCompletelyRightOfLine2_ShouldReturnFalse()
    {
        // Arrange
        var line1 = new Line(new Position(4, 0), new Position(4, 4), LineDirection.Vertical);
        var line2 = new Line(new Position(0, 0), new Position(2, 0), LineDirection.Horizontal);

        // Act
        var result = line1.HasCrossingOverlap(line2);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WhenLine1IsCompletelyAboveLine2_ShouldReturnFalse()
    {
        // Arrange
        var line1 = new Line(new Position(0, 0), new Position(2, 0), LineDirection.Horizontal);
        var line2 = new Line(new Position(4, 0), new Position(4, 4), LineDirection.Vertical);

        // Act
        var result = line1.HasCrossingOverlap(line2);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WhenLine1IsCompletelyBelowLine2_ShouldReturnFalse()
    {
        // Arrange
        var line1 = new Line(new Position(4, 0), new Position(4, 4), LineDirection.Vertical);
        var line2 = new Line(new Position(0, 0), new Position(2, 0), LineDirection.Horizontal);

        // Act
        var result = line1.HasCrossingOverlap(line2);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WhenLine1IsCrossingLine2_ShouldReturnTrue()
    {
        // Arrange
        var line1 = new Line(new Position(0, 0), new Position(4, 0), LineDirection.Horizontal);
        var line2 = new Line(new Position(2, 0), new Position(2, 4), LineDirection.Vertical);

        // Act
        var result = line1.HasCrossingOverlap(line2);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void WhenLine1IsTouchingLine2_ShouldReturnTrue()
    {
        // Arrange
        var line1 = new Line(new Position(0, 0), new Position(4, 0), LineDirection.Horizontal);
        var line2 = new Line(new Position(4, 0), new Position(4, 4), LineDirection.Vertical);

        // Act
        var result = line1.HasCrossingOverlap(line2);

        // Assert
        result.Should().BeTrue();
    }
}