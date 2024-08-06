using CleanerRobot.Application;
using CleanerRobot.Application.Models;
using CleanerRobot.Data;
using FluentAssertions;
using NSubstitute;

namespace Tests.UnitTests.Application.CleanerRobotServiceTests;

public class CommandCleanOfficeTests
{
    [Theory]
    [MemberData(nameof(TestIntersectionsData))]
    public void CleanWithIntersections_ShouldCalculateCorrectly(List<Command> commands, int expected)
    {
        var service = new CleanerRobotService(Substitute.For<IDbHandler>());

        var cleaningResult = service.CleanOffice(new CleaningRequest(new Point(0, 0), commands));

        cleaningResult.Result.Should().Be(expected);
        cleaningResult.Commands.Should().Be(commands.Count);
    }

    public static IEnumerable<object[]> TestIntersectionsData()
    {
        yield return new object[]
        {
            new List<Command>
            {
                new ("east", 10),
                new ("north", 2),
                new ("west", 10),
                new ("north", 2),
                new ("east", 10),
                new ("north", 2),
                new ("west", 5),
                new ("south", 20),
            },
            59
        };
        
        yield return new object[]
        {
            new List<Command>
            {
                new ("east", 10),
                new ("south", 2),
                new ("west", 10),
                new ("south", 2),
                new ("east", 10),
                new ("south", 2),
                new ("west", 5),
                new ("north", 20),
            },
            59
        };
        
        yield return new object[]
        {
            new List<Command>
            {
                new ("north", 10),
                new ("east", 2),
                new ("south", 10),
                new ("east", 2),
                new ("north", 10),
                new ("east", 2),
                new ("south", 5),
                new ("west", 20),
            },
            59
        };
        
        yield return new object[]
        {
            new List<Command>
            {
                new ("north", 10),
                new ("west", 2),
                new ("south", 10),
                new ("west", 2),
                new ("north", 10),
                new ("west", 2),
                new ("south", 5),
                new ("east", 20),
            },
            59
        };
        
        yield return new object[]
        {
            new List<Command>
            {
                new ("east", 10),
                new ("north", 10),
                new ("west", 10),
                new ("south", 10),
            },
            40
        };
    }
    
    [Fact]
    public void CleanWithNoIntersections_ShouldCalculateCorrectly()
    {
        var service = new CleanerRobotService(Substitute.For<IDbHandler>());

        var cleaningResult = service.CleanOffice(new CleaningRequest(new Point(0, 0), new List<Command>
        {
            new ("east", 10),
            new ("north", 2),
            new ("west", 10),
            new ("north", 2),
            new ("east", 10),
            new ("north", 2),
            new ("west", 20),
            new ("south", 2),
        }));

        cleaningResult.Result.Should().Be(59);
    }
    
    [Fact]
    public void CleanWithGivenSampleInTask_ShouldCalculateCorrectly()
    {
        var service = new CleanerRobotService(Substitute.For<IDbHandler>());

        var cleaningResult = service.CleanOffice(new CleaningRequest(new Point(0, 0), new List<Command>
        {
            new ("east", 2),
            new ("north", 1),
        }));

        cleaningResult.Result.Should().Be(4);
    }
    
    [Theory]
    [MemberData(nameof(TestOverlapsData))]
    public void CleanWithOverlaps_ShouldCalculateCorrectly(List<Command> commands, int expected)
    {
        var service = new CleanerRobotService(Substitute.For<IDbHandler>());

        var cleaningResult = service.CleanOffice(new CleaningRequest(new Point(0, 0), commands));

        cleaningResult.Result.Should().Be(expected);
        cleaningResult.Commands.Should().Be(commands.Count);
    }
    
    public static IEnumerable<object[]> TestOverlapsData()
    {
        yield return new object[]
        {
            new List<Command>
            {
                new ("east", 10),
                new ("west", 10),
                new ("north", 10),
                new ("south", 10),
                new ("west", 10),
                new ("east", 10),
                new ("south", 10),
                new ("north", 10),
            },
            41
        };
        
        yield return new object[]
        {
            new List<Command>
            {
                new ("east", 10),
                new ("west", 10),
                new ("west", 10),
                new ("east", 10),
                new ("south", 10),
                new ("north", 10),
                new ("north", 10),
                new ("south", 10),
            },
            41
        };
        
        yield return new object[]
        {
            new List<Command>
            {
                new ("east", 10),
                new ("north", 10),
                new ("east", 10),
                new ("south", 5),
                new ("west", 5),
                new ("north", 5),
                new ("east", 5),
            },
            45
        };
        
        yield return new object[]
        {
            new List<Command>
            {
                new ("south", 10),
                new ("west", 10),
                new ("east", 5),
                new ("north", 3),
                new ("east", 10),
            },
            33
        };
        
        yield return new object[]
        {
            new List<Command>
            {
                new ("south", 10),
                new ("east", 10),
                new ("west", 5),
                new ("north", 3),
                new ("west", 10),
            },
            33
        };
    }
    
    [Fact]
    public void CleanWithoutOverlaps_ShouldCalculateCorrectly()
    {
        var service = new CleanerRobotService(Substitute.For<IDbHandler>());

        var cleaningResult = service.CleanOffice(new CleaningRequest(new Point(0, 0), new List<Command>
        {
            new ("east", 10),
            new ("north", 2),
            new ("west", 10),
            new ("north", 2),
            new ("east", 10),
        }));

        cleaningResult.Result.Should().Be(35);
    }
}