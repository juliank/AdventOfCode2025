namespace AdventOfCode.Puzzles;

public class Puzzle07 : Puzzle<string, long>
{
    private const int PuzzleId = 07;

    public Puzzle07() : base(PuzzleId) { }

    public Puzzle07(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        // Replace the start indicator with a beam, for easier logic below
        InputEntries[0] = InputEntries[0].Replace('S', '|');
        
        var splitCount = 0L;
        for (var y = 0; y < Boundary.MaxY; y++)
        {
            for (var x = 0; x < Boundary.MaxX; x++)
            {
                var point = new Point(x, y);
                if (InputMap[point] == '|')
                {
                    var down = point.Get(Direction.S);
                    if (point.IsWithin(Boundary))
                    {
                        if (InputMap[down] == '.')
                        {
                            // Beam hits an empty space, the beam will continue
                            InputMap[down] = '|';
                        }
                        else if (InputMap[down] == '^')
                        {
                            // Beam hits a splitter: add a beam to both sides of the splitter
                            splitCount++;
                            var left = point.Get(Direction.SW);
                            var right = point.Get(Direction.SE);
                            foreach (var nextPoint in new [] { left, right })
                            {
                                if (nextPoint.IsWithin(Boundary) && InputMap[nextPoint] == '.')
                                {
                                    InputMap[nextPoint] = '|';
                                }
                            }
                        }
                    }
                }
            }
        }
        
        return splitCount;
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
