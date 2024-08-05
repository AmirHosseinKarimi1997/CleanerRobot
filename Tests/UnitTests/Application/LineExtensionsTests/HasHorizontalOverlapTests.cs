using CleanerRobot.Application;
using CleanerRobot.Application.Models;
using FluentAssertions;

namespace Tests.UnitTests.Application.LineExtensionsTests;

public class HasHorizontalOverlapTests
{
    [Theory]
    [MemberData(nameof(GetWhenLine1IsCompletelyTheOtherSideOfLine2TestData))]
    public void WhenLine1IsCompletelyTheOtherSideOfLine2_ShouldReturnFalse(Position startLine1, Position endLine1, Position startLine2, Position endLine2)
    {
        // Arrange
        var line1 = new Line(startLine1, endLine1, LineDirection.Horizontal);
        var line2 = new Line(startLine2, endLine2, LineDirection.Horizontal);

        // Act
        var result = line1.HasHorizontalOverlap(line2);

        // Assert
        result.Should().BeFalse();
    }

    public static IEnumerable<object[]> GetWhenLine1IsCompletelyTheOtherSideOfLine2TestData()
    {
        yield return [new Position(0, 0), new Position(1, 0), new Position(2, 0), new Position(3, 0)];
        yield return [new Position(2, 0), new Position(3, 0), new Position(-2, 0), new Position(1, 0)];
        yield return [new Position(0, 0), new Position(2, 0), new Position(0, 2), new Position(2, 2)];
        yield return [new Position(0, 2), new Position(3, 2), new Position(2, 0), new Position(4, 0)];
    }

    [Theory]
    [MemberData(nameof(GetWhenLine1IsTouchingTestData))]
    public void WhenLine1IsTouching_ShouldReturnTrue(Position startLine1, Position endLine1, Position startLine2, Position endLine2)
    {
        // Arrange
        var line1 = new Line(startLine1, endLine1, LineDirection.Horizontal);
        var line2 = new Line(startLine2, endLine2, LineDirection.Horizontal);

        // Act
        var result = line1.HasHorizontalOverlap(line2);

        // Assert
        result.Should().BeTrue();
    }
    
    public static IEnumerable<object[]> GetWhenLine1IsTouchingTestData()
    {
        yield return [new Position(1, 0), new Position(2, 0), new Position(0, 0), new Position(1, 0)];
        yield return [new Position(0, 0), new Position(2, 0), new Position(2, 0), new Position(4, 0)];
        yield return [new Position(0, 3), new Position(-5, 3), new Position(-5, 3), new Position(-10, 3)];
    }
    
    [Theory]
    [MemberData(nameof(GetWhenLine1OverlappingWithLine2_ShouldReturnTrueTestData))]
    public void WhenLine1OverlappingWithLine2_ShouldReturnTrue(Position startLine1, Position endLine1, Position startLine2, Position endLine2)
    {
        // Arrange
        var line1 = new Line(startLine1, endLine1, LineDirection.Horizontal);
        var line2 = new Line(startLine2, endLine2, LineDirection.Horizontal);

        // Act
        var result = line1.HasHorizontalOverlap(line2);

        // Assert
        result.Should().BeTrue();
    }
    
    public static IEnumerable<object[]> GetWhenLine1OverlappingWithLine2_ShouldReturnTrueTestData()
    {
        yield return [new Position(1, 0), new Position(3, 0), new Position(0, 0), new Position(2, 0)];
        yield return [new Position(0, 0), new Position(5, 0), new Position(2, 0), new Position(7, 0)];
        yield return [new Position(0, 0), new Position(10, 0), new Position(2, 0), new Position(5, 0)];
        yield return [new Position(0, 1), new Position(10, 1), new Position(20, 1), new Position(7, 1)];
    }

    [Fact]
    public void WhenLineIsNotHorizontal_ShouldReturnFalse()
    {
        // Arrange
        var line1 = new Line(new Position(0, 0), new Position(0, 1), LineDirection.Vertical);
        var line2 = new Line(new Position(0, 0), new Position(0, 1), LineDirection.Horizontal);

        // Act
        var result = line1.HasHorizontalOverlap(line2);

        // Assert
        result.Should().BeFalse();
    }
}