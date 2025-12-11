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
        // Replace the start indicator with a beam, for easier logic below
        InputEntries[0] = InputEntries[0].Replace('S', '|');

        Dictionary<Point, long> timelines = [];

        var firstTimelineInitialized = false;
        for (var y = 0; y < Boundary.MaxY; y++)
        {
            for (var x = 0; x <= Boundary.MaxX; x++)
            {
                var point = new Point(x, y);
                if (InputMap[point] == '|')
                {
                    if (!firstTimelineInitialized)
                    {
                        timelines[point] = 1;
                        firstTimelineInitialized = true;
                    }
                    var down = point.Get(Direction.S);
                    if (point.IsWithin(Boundary))
                    {
                        if (InputMap[down] is '.' or '|')
                        {
                            // Beam hits an empty space, the beam will continue
                            InputMap[down] = '|';
                            var timelineCount = timelines[point];
                            if (timelines.TryGetValue(down, out var currentCount))
                            {
                                timelineCount += currentCount;
                            }
                            timelines[down] = timelineCount;
                        }
                        else if (InputMap[down] == '^')
                        {
                            // Beam hits a splitter: add a beam to both sides of the splitter
                            var left = point.Get(Direction.SW);
                            var right = point.Get(Direction.SE);
                            foreach (var nextPoint in new [] { left, right })
                            {
                                if (nextPoint.IsWithin(Boundary) && InputMap[nextPoint] is '.' or '|')
                                {
                                    var timelineCount = timelines[point];
                                    if (timelines.TryGetValue(nextPoint, out var currentCount))
                                    {
                                        timelineCount += currentCount;
                                    }
                                    timelines[nextPoint] = timelineCount;
                                    InputMap[nextPoint] = '|';
                                }
                            }
                        }
                    }
                }
            }

            if (IsRunningFromTest)
            {
                // Debug output (will only work for single-digit numbers, which we seem to get with the example input)
                var row = InputMap.Where(kvp => kvp.Key.Y == y).ToList();
                var rowElements = row.Select(kvp => kvp.Value == '|' ? timelines[kvp.Key].ToString() : ".");
                var rowString = string.Join("", rowElements);
                Console.WriteLine(rowString);
            }
        }

        if (IsRunningFromTest)
        {
            var v = InputMap.Select(kvp => (kvp.Key, kvp.Value));
            Console.WriteLine();
            Helper.PrintMap(Boundary, v);
        }

        var finalRow = timelines.Where(kvp => kvp.Key.Y == Boundary.MaxY!);
        var timelineCounts = finalRow.Select(kvp => kvp.Value);
        var timelineSum = timelineCounts.Sum();
        return timelineSum;
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
