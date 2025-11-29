namespace AdventOfCode.Utils;

public static class AStarPathfinding
{
    /// <summary>
    /// Finds the shortest path between two points in a grid or graph using the A* algorithm.
    /// Calculates the optimal path based on the provided heuristic function
    /// and evaluates neighboring nodes to determine the most efficient route.
    /// </summary>
    /// <seealso href="https://en.wikipedia.org/wiki/A*_search_algorithm"/>
    /// <param name="start">The starting point of the path.</param>
    /// <param name="end">The end point of the path.</param>
    /// <param name="boundary">The boundaries to move within.</param>
    /// <param name="obstacles">Any obstacles that should be avoided.</param>
    /// <param name="costFunction">
    /// A custom function to calculate the cost of moving from one step to the next,
    /// taking the previous step into account. If not given, the default cost for
    /// moving to the next step will be 1.
    /// </param>
    /// <returns>A list of points representing the shortest path from the start to the goal, or an empty list if no path is found.</returns>
    public static List<Point> FindShortestPath(
    Point start,
    Point end,
    Boundary boundary,
    HashSet<Point> obstacles,
    Func<Point?, Point, Point, int>? costFunction = null)
    {
        int FScoreComparison((int FScore, Point Point) a, (int FScore, Point Point) b)
        {
            return a.FScore == b.FScore
                ? (a.Point.X == b.Point.X ? a.Point.Y - b.Point.Y : a.Point.X - b.Point.X)
                : a.FScore - b.FScore;
        }

        var fScoreComparer = Comparer<(int FScore, Point Point)>.Create(FScoreComparison);
        var openSet = new SortedSet<(int FScore, Point Point)>(fScoreComparer);

        var gScore = new Dictionary<Point, int>();
        var fScore = new Dictionary<Point, int>();
        var cameFrom = new Dictionary<Point, Point>();

        gScore[start] = 0;
        // Diagonal traversal is not possible, so the Manhattan distance is a good heuristic to use
        fScore[start] = start.ManhattanDistanceTo(end);
        openSet.Add((fScore[start], start));

        while (openSet.Count > 0)
        {
            var current = openSet.Min.Point;
            openSet.Remove(openSet.Min);

            if (current == end)
            {
                return ReconstructPath(cameFrom, current);
            }

            var neighbors = Directions.D2
                .Select(current.Get)
                .Where(p => p.IsWithin(boundary) && !obstacles.Contains(p))
                .ToList();
            foreach (var neighbor in neighbors)
            {
                int tentativeGScore;
                if (costFunction == null)
                {
                    tentativeGScore = gScore[current] + 1;
                }
                else
                {
                    Point? previous = null;
                    if (cameFrom.TryGetValue(current, out var prev))
                    {
                        previous = prev;
                    }
                    tentativeGScore = gScore[current] + costFunction(previous, current, neighbor);
                }

                if (gScore.TryGetValue(neighbor, out int neighborGScore) && tentativeGScore >= neighborGScore)
                {
                    continue;
                }

                cameFrom[neighbor] = current;
                neighborGScore = tentativeGScore;
                gScore[neighbor] = neighborGScore;
                fScore[neighbor] = tentativeGScore + neighbor.ManhattanDistanceTo(end);

                // No need to check Contains, since adding an item that already exists will do nothing
                openSet.Add((fScore[neighbor], neighbor));
            }
        }

        return []; // No path found
    }

    private static List<Point> ReconstructPath(Dictionary<Point, Point> cameFrom, Point current)
    {
        var path = new List<Point>
        {
            current
        };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
}
