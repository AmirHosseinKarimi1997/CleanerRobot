namespace CleanerRobot.Application.Models;

public class CleanedPath
{
    private readonly Dictionary<int, List<Line>> _horizontalLines = new(); //key is x-axis
    private readonly Dictionary<int, List<Line>> _verticalLines = new(); //key is y-axis
    
    public IReadOnlyDictionary<int, List<Line>> HorizontalLines => _horizontalLines;
    public IReadOnlyDictionary<int, List<Line>> VerticalLines => _verticalLines;
    
    public void Add(Line line)
    {
        if (line.IsHorizontal())
        {
            var horizontalLines = _horizontalLines.GetValueOrDefault(line.Start.Y, new List<Line>());
            horizontalLines.Add(line);
            _horizontalLines[line.Start.Y] = horizontalLines;
        }
        else if (line.IsVertical())
        {
            var verticalLines = _verticalLines.GetValueOrDefault(line.Start.X, new List<Line>());
            verticalLines.Add(line);
            _verticalLines[line.Start.X] = verticalLines;
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}