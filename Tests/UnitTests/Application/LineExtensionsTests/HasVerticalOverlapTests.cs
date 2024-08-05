using CleanerRobot.Application;
using CleanerRobot.Application.Models;
using FluentAssertions;

namespace Tests.UnitTests.Application.LineExtensionsTests;

public class HasVerticalOverlapTests
{
    [Fact]
    public void WhenLine1IsCompletelyAboveLine2_ShouldReturnFalse()
    {
        // Arrange
        var line1 = new Line(new Position(0, 0), new Position(0, 1), LineDirection.Vertical);
        var line2 = new Line(new Position(0, 2), new Position(0, 3), LineDirection.Vertical);

        // Act
        var result = line1.HasVerticalOverlap(line2);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WhenLine1IsCompletelyBelowLine2_ShouldReturnFalse()
    {
        // Arrange
        var line1 = new Line(new Position(0, 2), new Position(0, 3), LineDirection.Vertical);
        var line2 = new Line(new Position(0, 0), new Position(0, 1), LineDirection.Vertical);

        // Act
        var result = line1.HasVerticalOverlap(line2);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WhenLine1IsCompletelyLeftOfLine2_ShouldReturnFalse()
    {
        // Arrange
        var line1 = new Line(new Position(0, 0), new Position(0, 1), LineDirection.Vertical);
        var line2 = new Line(new Position(1, 0), new Position(1, 1), LineDirection.Vertical);

        // Act
        var result = line1.HasVerticalOverlap(line2);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WhenLine1IsCompletelyRightOfLine2_ShouldReturnFalse()
    {
        // Arrange
        var line1 = new Line(new Position(1, 0), new Position(1, 1), LineDirection.Vertical);
        var line2 = new Line(new Position(0, 0), new Position(0, 1), LineDirection.Vertical);

        // Act
        var result = line1.HasVerticalOverlap(line2);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WhenLine1IsAboveLine2_ShouldReturnTrue()
    {
        // Arrange
        var line1 = new Line(new Position(0, 0), new Position(0, 2), LineDirection.Vertical);
        var line2 = new Line(new Position(0, 1), new Position(0, 3), LineDirection.Vertical);

        // Act
        var result = line1.HasVerticalOverlap(line2);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void WhenLine1IsBelowLine2_ShouldReturnTrue()
    {
        // Arrange
        var line1 = new Line(new Position(0, 1), new Position(0, 3), LineDirection.Vertical);
        var line2 = new Line(new Position(0, 0), new Position(0, 2), LineDirection.Vertical);

        // Act
        var result = line1.HasVerticalOverlap(line2);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void WhenLine1IsLeftOfLine2_ShouldReturnFalse()
    {
        // Arrange
        var line1 = new Line(new Position(0, 0), new Position(0, 2), LineDirection.Vertical);
        var line2 = new Line(new Position(1, 0), new Position(1, 2), LineDirection.Vertical);

        // Act
        var result = line1.HasVerticalOverlap(line2);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WhenLine1IsRightOfLine2_ShouldReturnFalse()
    {
        // Arrange
        var line1 = new Line(new Position(1, 0), new Position(1, 2), LineDirection.Vertical);
        var line2 = new Line(new Position(0, 0), new Position(0, 2), LineDirection.Vertical);

        // Act
        var result = line1.HasVerticalOverlap(line2);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WhenLine1IsCompletelyAboveLine2ButOverlapping_ShouldReturnTrue()
    {
        // Arrange
        var line1 = new Line(new Position(0, 0), new Position(0, 2), LineDirection.Vertical);
        var line2 = new Line(new Position(0, 1), new Position(0, 3), LineDirection.Vertical);

        // Act
        var result = line1.HasVerticalOverlap(line2);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void WhenLine1IsCompletelyBelowLine2ButOverlapping_ShouldReturnTrue()
    {
        // Arrange
        var line1 = new Line(new Position(0, 1), new Position(0, 3), LineDirection.Vertical);
        var line2 = new Line(new Position(0, 0), new Position(0, 2), LineDirection.Vertical);

        // Act
        var result = line1.HasVerticalOverlap(line2);

        // Assert
        result.Should().BeTrue();
    }
}