using CleanerRobot.Application;
using CleanerRobot.Application.Models;
using CleanerRobot.Entity;
using Microsoft.AspNetCore.Mvc;

namespace CleanerRobot.Controllers;

[ApiController]
[Route("[controller]")]
public class TibberDeveloperTestController : ControllerBase
{
    private readonly ICleanerRobotService _cleanerRobotService;
    private readonly ILogger<TibberDeveloperTestController> _logger;

    public TibberDeveloperTestController(ICleanerRobotService cleanerRobotService, ILogger<TibberDeveloperTestController> logger)
    {
        _cleanerRobotService = cleanerRobotService;
        _logger = logger;
    }

    [HttpPost(Name = "enter-path")]
    public CleaningResult CleanOffice([FromBody] CleaningRequest request)
    {
        return _cleanerRobotService.CommandCleanOffice(request);
    }
}