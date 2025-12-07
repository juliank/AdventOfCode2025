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
        var repeatRow = false; // If a beam has hit a splitter, we must repeat the row to process the newly split beams
        for (var y = 0; y < Boundary.MaxY; y++)
        {
            var secondTraversal = repeatRow;
            repeatRow = false;
            
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
                            // Beam hits a splitter. We use 'x' to indicate an "activated" splitter
                            InputMap[down] = 'x';
                        }
                    }
                }
                else if (InputMap[point] == 'x' && !secondTraversal)
                {
                    splitCount++;
                    repeatRow = true;
                    
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
