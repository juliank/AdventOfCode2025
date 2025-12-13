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
    }

    public override long SolvePart2()
    {
        // throw new HardCodedResultException(1603439684, "Solution time: 00:06:32.8642290"); // Running in release mode, consuming 9 GB memory
        _logProgress = true;
        var redTiles = InputEntries;
        var borderList = new List<(int X, int Y)>();
        
        // The border will wrap around, so we use the last point as the first previous one
        var previousPoint = (redTiles[^1].X, redTiles[^1].Y);
        
        foreach (var point in redTiles)
        {
            if (point.X == previousPoint.X)
            {
                // The points are on a vertical line
                var increment = point.Y > previousPoint.Y ? 1 : -1;
                for (var y = previousPoint.Y + increment; y != point.Y; y += increment)
                {
                    borderList.Add((point.X, y));
                }
            }
            else // point.Y == previousPoint.Y
            {
                // The points are on a horizontal line
                var increment = point.X > previousPoint.X ? 1 : -1;
                for (var x = previousPoint.X + increment; x != point.X; x += increment)
                {
                    borderList.Add((x, point.Y));
                }
            }

            previousPoint = (point.X, point.Y);
            borderList.Add(previousPoint);
        }
        
        var minX = borderList.MinBy(p => p.X).X;
        var maxX = borderList.MaxBy(p => p.X).X + 2;
        var minY = borderList.MinBy(p => p.Y).Y;
        var maxY = borderList.MaxBy(p => p.Y).Y + 1;
        var border = borderList.ToHashSet(); // Convert the border to a hash set for faster lookup
        
        LogProgress($"{borderList.Count} points on the border between ({minX:N0},{minY:N0}) and ({maxX:N0},{maxY:N0})");
        
        // Will, strictly speaking, also include the red corner tiles, but we consider them all green for simplicity
        var greenTiles = new bool[maxY][]; // All will be initialized to false
        greenTiles[minY - 1] = new bool[maxX]; // No need to initialize any row before one above the first point
        for (var y = minY; y < maxY; y++)
        {
            if (y % 1_000 == 0)
            {
                LogProgress($"Analyzing green tiles for row {y:N0}");
            }
            greenTiles[y] = new bool[maxX];
            for (var x = minX; x < maxX; x++)
            {
                if (border.Contains((x, y)))
                {
                    // We're on the border: this tile is definitely green
                    greenTiles[y][x] = true;
                    continue;
                }

                if (!greenTiles[y - 1][x])
                {
                    // We're not on the border, and the tile above is not green: this tile is definitely not green
                    continue;
                }
                
                // The above tile is green, we have to check some more ...
                
                if (!border.Contains((x - 1, y)))
                {
                    // The previous tile was not on the border: we've not crossed the border, so this tile is the same as the previous
                    greenTiles[y][x] = greenTiles[y][x - 1];
                    continue;
                }
                
                // The above tile is green, and the previous tile was on the border, we have to check some more ...
                
                if (!border.Contains((x, y - 1)))
                {
                    // The above tile was not on the border: we've not crossed the border, so this tile is the same as the above - which is green
                    greenTiles[y][x] = true;
                    continue;
                }
                
                // The opposite of one above and two to the left
                greenTiles[y][x] = !greenTiles[y - 1][x - 2];
                
                
                // if (border.Contains(((y - 1), (x - 1))) && border.Contains(((y + 1), (x - 1))))
                // {
                //     // We've just crossed a vertical border: this tile is also green (as the one above)
                //     //  . | * 
                //     //  . | ? 
                //     //  . | 
                //     greenTiles[y][x] = true;
                //     continue;
                // }
                //
                // if (border.Contains(((y), (x - 2))) &&
                //     (border.Contains(((y - 1), (x - 1))) || border.Contains(((y + 1), (x - 1)))))
                // {
                //     //  . | *    OR   //  * * *
                //     //  --| ?         //  --| ?
                //     //  . .           //  . |
                //     greenTiles[y][x] = true;
                //     continue;
                // }
                //
                // // The tile above and to the left of the previous is green: this one is not
                // if (greenTiles[y - 1][x - 2])
                // {
                //     // Current tile is ? - other tiles are either on the border (| or -) or inside (*) - all of which are green
                //     //  * * *
                //     //  * |--
                //     //  * | ?
                //     continue;
                // }
                //
                // if (border.Contains(((y - 1), (x))))
                // {
                //     // We're in a corner with the border above and to the left: this tile will be
                //     // the opposite of the tile above and two to the left
                //     //  . |--    OR     * |-- 
                //     //  --| ?           --| ?  
                //     greenTiles[y][x] = !greenTiles[y - 1][x - 2];
                // }
                
            }
            // end for x
        }
        // end for y
        
        var maxArea = 0L;
        
        // Just for debugging
        var boundary = new Boundary(minX - 1, minY - 1, maxX + 1, maxY + 1);
        
        // We'll go through all possible rectangles formed by two red tiles and see if they are fully within the green tiles
        var permutations = redTiles.Count * (redTiles.Count - 1) / 2;
        var permutationCount = 0;
        var logFrequency = permutations / 100;
        logFrequency = Math.Max(logFrequency, 1); // To avoid divide by zero for small example input
        LogProgress($"There are {permutations:N0} possible rectangles");
        for (var a = 0; a < redTiles.Count; a++)
        {
            for (var b = a + 1; b < redTiles.Count; b++)
            {
                var pointA = redTiles[a];
                var pointB = redTiles[b];
                
                if (IsRunningFromTest)
                {
                    IEnumerable<(Point P, char C)> points = borderList.Select(p =>
                    {
                        var c = p.X == pointA.X && p.Y == pointA.Y ? 'A' : (p.X == pointB.X && p.Y == pointB.Y ? 'B' : '.');
                        return (new Point(p.X, p.Y), c);
                    });
                    var ar = CalculateArea(pointA, pointB);
                    Console.WriteLine($"\nArea: {ar} - Checking if both A ({pointA}) and B ({pointB}) are within the boundary");
                    Helper.PrintMap(boundary, points, printBorder: true);
                    PrintGreenTiles();
                }
                
                permutationCount++;
                if (permutationCount % logFrequency == 0)
                {
                    LogProgress($"Processing permutation {permutationCount:N0}/{permutations:N0}");
                }
                if (!RectangleIsGreen(pointA, pointB))
                {
                    continue;
                }

                var area = CalculateArea(pointA, pointB);
                maxArea = Math.Max(maxArea, area);
            }
        }
        LogProgress($"Finished processing {permutationCount:N0}/{permutations:N0} permutations");
        
        return maxArea;
        // Solution time: 00:11:36.5046630
        // Result is: [179674176]
        // That's not the right answer; your answer is too low.
        
        void PrintGreenTiles()
        {
            Console.WriteLine();
            foreach (var row in greenTiles)
            {
                var line = string.Join("", row.Select(g => g ? "." : " "));
                Console.WriteLine(line);
            }
        }

        bool RectangleIsGreen(Point a, Point b)
        {
            // If *any* of the points between A and B are not a green point, then we can be sure that
            // the whole rectangle does *not* consist only of green points
            
            // Loop first horizontal lines
            var increment = b.X > a.X ? 1 : -1;
            for (var x = a.X; x != b.X; x += increment)
            {
                if (!greenTiles[a.Y][x] || !greenTiles[b.Y][x])
                {
                    return false;
                }
            }
            
            // The loop vertical lines
            increment = b.Y > a.Y ? 1 : -1;
            for (var y = a.Y; y != b.Y; y += increment)
            {
                // if (!availableArea.Contains(pointOnLineFromA.X, pointOnLineFromA.Y))
                if (!greenTiles[y][a.X] || !greenTiles[y][b.X])
                {
                    return false;
                }
            }
            
            // All the line points are green, which means that the whole rectangle is green
            return true;
        }
    }

    private static long CalculateArea(Point pointA, Point pointB)
    {
        var length = Math.Abs(pointA.X - pointB.X) + 1;
        var width = Math.Abs(pointA.Y - pointB.Y) + 1;
        var area = (long)length * width;
        return area;
    }

    protected internal override Point ParseInput(string inputItem)
    {
        var parts = inputItem.Split(',');
        var point = new Point(int.Parse(parts[0]), int.Parse(parts[1]));
        return point;
    }
}
