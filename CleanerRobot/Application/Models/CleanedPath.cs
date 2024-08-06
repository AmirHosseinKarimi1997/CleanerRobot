namespace CleanerRobot.Application.Models;

public class CleanedPath
{
    private readonly Dictionary<int, List<Line>> _horizontalLines = new(); //key is y-axis
    private readonly Dictionary<int, List<Line>> _verticalLines = new(); //key is x-axis
    
    public IReadOnlyDictionary<int, List<Line>> HorizontalLines => _horizontalLines;
    public IReadOnlyDictionary<int, List<Line>> VerticalLines => _verticalLines;
    
    public void Add(Line line)
    {
        if (line.IsHorizontal())
        {
            var horizontalLinesWithSameYAxis = _horizontalLines.GetValueOrDefault(line.Start.Y, new List<Line>());
            horizontalLinesWithSameYAxis.Add(line);
            _horizontalLines[line.Start.Y] = horizontalLinesWithSameYAxis;
        }
        else if (line.IsVertical())
        {
            var verticalLinesWithSameXAxis = _verticalLines.GetValueOrDefault(line.Start.X, new List<Line>());
            verticalLinesWithSameXAxis.Add(line);
            _verticalLines[line.Start.X] = verticalLinesWithSameXAxis;
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}