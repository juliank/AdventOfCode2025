namespace AdventOfCode.Puzzles;

public class Puzzle04 : Puzzle<string, long>
{
    private const int PuzzleId = 04;

    public Puzzle04() : base(PuzzleId) { }

    public Puzzle04(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var rows = InputEntries[0].Length;
        var columns = InputEntries.Count;

        var movableRolls = 0;
        for (var x = 0; x < rows; x++)
        {
            for (var y = 0; y < columns; y++)
            {
                var point = new Point(x, y);
                var candidate = InputMap[point];
                if (candidate != '@')
                {
                    continue;
                }

                var neighborRolls = 0;
                foreach (var direction in Directions.D2Extended)
                {
                    Point neighbor = point.Get(direction);
                    if (!InputMap.TryGetValue(neighbor, out var neighborValue))
                    {
                        continue;
                    }

                    if (neighborValue == '@')
                    {
                        neighborRolls++;
                        if (neighborRolls == 4)
                        {
                            break;
                        }
                    }
                }

                if (neighborRolls < 4)
                {
                    movableRolls++;
                }
            }
        }
        
        return movableRolls;
    }

    public override long SolvePart2()
    {
        var movableRolls = GetMovableRolls();

        var removedRolls = 0;
        while (movableRolls.Count > 0)
        {
            removedRolls += movableRolls.Count;
            foreach (var roll in movableRolls)
            {
                InputMap[roll] = '.';
            }

            movableRolls = GetMovableRolls();
        }

        return removedRolls;
    }
    
    private List<Point> GetMovableRolls()
    {
        var rows = InputEntries[0].Length;
        var columns = InputEntries.Count;

        var movableRolls = new List<Point>();
        for (var x = 0; x < rows; x++)
        {
            for (var y = 0; y < columns; y++)
            {
                var point = new Point(x, y);
                if (InputMap[point] != '@')
                {
                    continue;
                }

                var neighborRolls = 0;
                foreach (var direction in Directions.D2Extended)
                {
                    Point neighbor = point.Get(direction);
                    if (!InputMap.TryGetValue(neighbor, out var neighborValue))
                    {
                        continue;
                    }

                    if (neighborValue == '@')
                    {
                        neighborRolls++;
                        if (neighborRolls == 4)
                        {
                            break;
                        }
                    }
                }

                if (neighborRolls < 4)
                {
                    movableRolls.Add(point);
                }
            }
        }

        return movableRolls;
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
