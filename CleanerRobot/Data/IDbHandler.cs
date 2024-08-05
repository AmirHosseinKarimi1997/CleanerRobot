using CleanerRobot.Entity;

namespace CleanerRobot.Data;

public interface IDbHandler
{
    void InsertCleaningResult(CleaningResult cleaningResult);
}