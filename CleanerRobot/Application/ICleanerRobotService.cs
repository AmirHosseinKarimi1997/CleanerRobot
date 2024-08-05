using CleanerRobot.Application.Models;
using CleanerRobot.Entity;

namespace CleanerRobot.Application;

public interface ICleanerRobotService
{
    CleaningResult CommandCleanOffice(CleaningRequest request);
}