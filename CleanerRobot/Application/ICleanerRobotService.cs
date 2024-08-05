using CleanerRobot.Application.Models;
using CleanerRobot.Entity;

namespace CleanerRobot.Application;

public interface ICleanerRobotService
{
    CleaningResult CleanOffice(CleaningRequest request);
}