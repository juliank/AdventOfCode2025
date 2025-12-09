namespace AdventOfCode.Puzzles;

public class Puzzle09 : Puzzle<Point, long>
{
    private const int PuzzleId = 09;

    public Puzzle09() : base(PuzzleId) { }

    public Puzzle09(params IEnumerable<Point> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var maxArea = 0L;
        
        for (var firstPoint = 0; firstPoint < InputEntries.Count; firstPoint++)
        {
            for (var secondPoint = firstPoint + 1; secondPoint < InputEntries.Count; secondPoint++)
            {
                var length = Math.Abs(InputEntries[firstPoint].X - InputEntries[secondPoint].X) + 1;
                var width = Math.Abs(InputEntries[firstPoint].Y - InputEntries[secondPoint].Y) + 1;
                var area = (long)length * width;
                maxArea = Math.Max(maxArea, area);
            }
        }

        return maxArea;
        // 4759282418 is too low
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    protected internal override Point ParseInput(string inputItem)
    {
        var parts = inputItem.Split(',');
        var point = new Point(int.Parse(parts[0]), int.Parse(parts[1]));
        return point;
    }
}
