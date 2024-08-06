using CleanerRobot.Entity;

namespace CleanerRobot.Data;

public interface ICleaningResultRepository
{
    void Add(CleaningResult cleaningResult);
}