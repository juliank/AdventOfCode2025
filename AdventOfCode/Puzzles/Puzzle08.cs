namespace AdventOfCode.Puzzles;

public class Puzzle08 : Puzzle<Point, long>
{
    // Default is 1000, but we must be able to override for test input
    public int JunctionBoxesToConnect { get; set; } = 1000;
    
    private const int PuzzleId = 08;

    public Puzzle08() : base(PuzzleId) { }

    public Puzzle08(params IEnumerable<Point> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var distances = new List<(float Distance, Point A, Point B)>();
        
        for (var fromIndex = 0; fromIndex < InputEntries.Count; fromIndex++)
        {
            var from = InputEntries[fromIndex];
            for (var toIndex = fromIndex + 1; toIndex < InputEntries.Count; toIndex++)
            {
                var to = InputEntries[toIndex];
                var distance = from.EuclideanDistanceTo(to);
                distances.Add((distance, from, to));
            }
        }

        distances = distances.OrderBy(d => d.Distance).Take(JunctionBoxesToConnect).ToList();

        var circuits = new List<List<Point>>();
        foreach (var distance in distances)
        {
            var circuitMatches = circuits.FindAll(c => c.Contains(distance.A) || c.Contains(distance.B));
            if (circuitMatches.Count == 0)
            {
                // New circuit
                circuits.Add([distance.A, distance.B]);
                continue;
            }

            if (circuitMatches.Count == 2)
            {
                // Merge two existing circuits
                var firstCircuit =  circuitMatches[0];
                var secondCircuit = circuitMatches[1];
                circuits.Remove(firstCircuit);
                circuits.Remove(secondCircuit);
                firstCircuit.AddRange(secondCircuit);
                circuits.Add(firstCircuit);
                continue;
            }

            var circuitMatch = circuitMatches[0];
            if (circuitMatch.Contains(distance.A) && circuitMatch.Contains(distance.B))
            {
                // Already in the same circuit, do nothing...
                continue;
            }

            circuitMatch.Add(circuitMatch.Contains(distance.A) ? distance.B : distance.A);
        }

        var circuitLengths = circuits.Select(c => (long)c.Count).OrderByDescending(l => l).Take(3).ToArray();
        var productOfLargestThree = circuitLengths.Aggregate((a, b) => a * b);
        return productOfLargestThree;
    }

    public override long SolvePart2()
    {
        var distances = new List<(float Distance, Point A, Point B)>();
        
        for (var fromIndex = 0; fromIndex < InputEntries.Count; fromIndex++)
        {
            var from = InputEntries[fromIndex];
            for (var toIndex = fromIndex + 1; toIndex < InputEntries.Count; toIndex++)
            {
                var to = InputEntries[toIndex];
                var distance = from.EuclideanDistanceTo(to);
                distances.Add((distance, from, to));
            }
        }

        distances = distances.OrderBy(d => d.Distance).ToList();

        var circuits = new List<List<Point>>();
        Point finalA = default;
        Point finalB = default;
        foreach (var distance in distances)
        {
            var circuitMatches = circuits.FindAll(c => c.Contains(distance.A) || c.Contains(distance.B));
            if (circuitMatches.Count == 0)
            {
                // New circuit
                circuits.Add([distance.A, distance.B]);
                continue;
            }

            if (circuitMatches.Count == 2)
            {
                // Merge two existing circuits
                var firstCircuit =  circuitMatches[0];
                var secondCircuit = circuitMatches[1];
                circuits.Remove(firstCircuit);
                circuits.Remove(secondCircuit);
                firstCircuit.AddRange(secondCircuit);
                circuits.Add(firstCircuit);
                if (circuits.Count > 1)
                {
                    // We've still not connected all circuits, so we must continue
                    continue;
                }

                // All circuits are connected. But it might not be "the last single" circuit,
                // as more circuits might be added in later iterations.
                finalA = distance.A;
                finalB = distance.B;
                continue;
            }

            var circuitMatch = circuitMatches[0];
            if (circuitMatch.Contains(distance.A) && circuitMatch.Contains(distance.B))
            {
                // Already in the same circuit, do nothing...
                continue;
            }

            circuitMatch.Add(circuitMatch.Contains(distance.A) ? distance.B : distance.A);
            if (circuitMatch.Count == InputEntries.Count)
            {
                // All points are now added to the same circuit
                finalA = distance.A;
                finalB = distance.B;
                break;
            }
        }

        var xProductOfLastPoints = (long)finalA.X * finalB.X;
        return xProductOfLastPoints;
    }

    protected internal override Point ParseInput(string inputItem)
    {
        var position = inputItem.Split(',').Select(int.Parse).ToArray();
        var point = new Point(position[0], position[1], position[2]);
        return point;
    }
}
