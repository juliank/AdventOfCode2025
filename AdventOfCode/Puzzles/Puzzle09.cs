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
        var redTiles = InputEntries;
        var border = new List<Point>();
        
        // The border will wrap around, so we use the last point as the first previous one
        var previousPoint = redTiles[^1];
        
        foreach (var point in redTiles)
        {
            if (point.X == previousPoint.X)
            {
                // The points are on a vertical line
                var increment = point.Y > previousPoint.Y ? 1 : -1;
                for (var y = previousPoint.Y + increment; y != point.Y; y += increment)
                {
                    var greenTile = new Point(point.X, y);
                    border.Add(greenTile);
                }
            }
            else // point.Y == previousPoint.Y
            {
                // The points are on a horizontal line
                var increment = point.X > previousPoint.X ? 1 : -1;
                for (var x = previousPoint.X + increment; x != point.X; x += increment)
                {
                    var greenTile = new Point(x, point.Y);
                    border.Add(greenTile);
                }
            }

            border.Add(point);
            previousPoint = point;
        }
        
        var maxArea = 0L;
        
        var minX = border.MinBy(p => p.X).X;
        var maxX = border.MaxBy(p => p.X).X;
        var minY = border.MinBy(p => p.Y).Y;
        var maxY = border.MaxBy(p => p.Y).Y;

        // Make the initial hash set as large as possible, since reallocations when we hit the current max size
        // will be expensive. And with the real data set, if starting from zero, this would happen a lot!
        var xSize = (long)maxX - minX;
        var ySize = maxY - minY;
        var maxSize = xSize * ySize;
        // The real input will yield a theoretical max size much larger than int.MaxValue, but for when running
        // tests we keep this as small as possible
        var size = maxSize > int.MaxValue ? int.MaxValue : (int)maxSize;
        // var availableArea = new HashSet<Point>(size);
        var availableArea = new HashSet<Point>();
        
        // Just for debugging
        var boundary = new Boundary(minX - 1, minY - 1, maxX + 1, maxY + 1);
        
        for (var y = minY; y <= maxY; y++)
        {
            // Go through all points on the current Y-row and add them to the available area if they are inside the border
            var borderRow = border.Where(p => p.Y == y).ToList();
            minX = borderRow.MinBy(p => p.X).X;
            maxX = borderRow.MaxBy(p => p.X).X;

            // When entering the border at a corner, we must keep track of the "entering direction", so that we can
            // decide if we've actually entered or left the enclosed area.
            // For a corner like └ we enter from the north, and for a corner like ┌ we enter from the south.
            Direction? borderEnterDirection = null;
            var enteringFromOutside = false;
            var inside = false;
            for (var x = minX; x <= maxX; x++)
            {
                var current = new  Point(x, y);

                if (!inside && !border.Contains(current))
                {
                    // We're still on the outside, nothing more to do here
                    continue;
                }
                
                if (inside && !border.Contains(current))
                {
                    // We're already inside, and not crossing/touching the border, so we're still inside
                    availableArea.Add(current);
                    continue;
                }

                var previous = current.GetW();
                var next = current.GetE();
                var north = current.GetN();
                var south = current.GetS();
                
                if (inside && border.Contains(current))
                {
                    // We're either traversing the border or about to enter the border from within
                    availableArea.Add(current);
                    
                    if (border.Contains(north) && border.Contains(south))
                    {
                        // We've about to cross the border, so the next one will be outside
                        inside = false;
                        borderEnterDirection = null;
                        continue;
                    }

                    if (border.Contains(previous) && border.Contains(next))
                    {
                        // We're (still) traversing the border
                        continue;
                    }

                    if (border.Contains(next))
                    {
                        // We're entering the border from within ...
                        enteringFromOutside = false;
                        
                        if (border.Contains(south))
                        {
                            // ... on a corner coming from the south
                            borderEnterDirection = Direction.S;
                        }
                        else if (border.Contains(north))
                        {
                            // ... on a corner coming from the north
                            borderEnterDirection = Direction.N;
                        }
                        else
                        {
                            throw new Exception("This should never happen #1");
                        }
                    }
                    else if (enteringFromOutside)
                    {
                        // We're about to leave the border after entering it from the outside ...
                        if ((borderEnterDirection == Direction.S && border.Contains(north)) ||
                            (borderEnterDirection == Direction.N && border.Contains(south)))
                        {
                            // ... in the opposite direction of what we entered, which means that we're about to
                            // cross the border and the next point will be inside
                            inside = true;
                        }
                        else if (borderEnterDirection == null)
                        {
                            throw new Exception("This should never happen #2");
                        }
                        // else: we've left the border in the same direction as we entered: we're still inside
                        borderEnterDirection = null;
                    }
                    else // !enteringFromOutside
                    {
                        // We're about to leave the border after entering it from the inside ...
                        if ((borderEnterDirection == Direction.S && border.Contains(north)) ||
                            (borderEnterDirection == Direction.N && border.Contains(south)))
                        {
                            // ... in the opposite direction of what we entered, which means that we're about to
                            // cross the border and the next point will be outside
                            inside = false;
                        }
                        else if (borderEnterDirection == null)
                        {
                            throw new Exception("This should never happen #3");
                        }
                        // else: we've left the border in the same direction as we entered: we're still inside
                        borderEnterDirection = null;
                    }
                    continue;
                }
                
                if (!inside && border.Contains(current))
                {
                    // We're about to enter the border from the outside
                    enteringFromOutside = true;
                    inside = true;
                    availableArea.Add(current);
                    
                    if (border.Contains(north) && border.Contains(south))
                    {
                        // We've about to cross the border, so the next one will be fully inside
                        borderEnterDirection = null;
                        continue;
                    }

                    if (!border.Contains(next))
                    {
                        throw new Exception("This should never happen #4");
                    }
                    
                    // We're entering the border ...
                    if (border.Contains(south))
                    {
                        // ... on a corner coming from the south
                        borderEnterDirection = Direction.S;
                    }
                    else if (border.Contains(north))
                    {
                        // ... on a corner coming from the north
                        borderEnterDirection = Direction.N;
                    }
                    else
                    {
                        throw new Exception("This should never happen #1");
                    }
                }
            }
        }

        if (IsRunningFromTest)
        {
            Helper.PrintMap(boundary, availableArea, c: '.', printBorder: true);
        }
        
        // We'll go through row by row and all points that are within (or on) the border to the set of available points
        for (var firstPoint = 0; firstPoint < InputEntries.Count; firstPoint++)
        {
            // for (var secondPoint = firstPoint + 1; secondPoint < border.Count; secondPoint++)
            for (var secondPoint = firstPoint + 1; secondPoint < InputEntries.Count; secondPoint++)
            {
                // var pointA = border[firstPoint];
                // var pointB = border[secondPoint];
                var pointA = InputEntries[firstPoint];
                var pointB = InputEntries[secondPoint];
                
                if (IsRunningFromTest)
                {
                    IEnumerable<(Point P, char C)> points = border.Select(p =>
                    {
                        var c = p == pointA ? 'A' : (p == pointB ? 'B' : '.');
                        return (p, c);
                    });
                    var a = CalculateArea(pointA, pointB);
                    Console.WriteLine($"\nArea: {a} - Checking if both A ({pointA}) and B ({pointB}) are within the boundary");
                    Helper.PrintMap(boundary, points, printBorder: true);
                }
                
                if (!RectangleIsWithin(pointA, pointB))
                {
                    continue;
                }

                var area = CalculateArea(pointA, pointB);
                maxArea = Math.Max(maxArea, area);
            }
        }
        
        
        return maxArea;

        bool RectangleIsWithin(Point a, Point b)
        {
            // Loop first horizontal lines
            var increment = b.X > a.X ? 1 : -1;
            for (var x = a.X; x != b.X; x += increment)
            {
                var pointOnLineFromA = new Point(x, a.Y);
                if (!availableArea.Contains(pointOnLineFromA))
                {
                    return false;
                }
                var pointOnLineToB = new Point(x, b.Y);
                if (!availableArea.Contains(pointOnLineToB))
                {
                    return false;
                }
            }
            // The loop vertical lines
            increment = b.Y > a.Y ? 1 : -1;
            for (var y = a.Y; y != b.Y; y += increment)
            {
                var pointOnLineFromA = new Point(a.X, y);
                if (!availableArea.Contains(pointOnLineFromA))
                {
                    return false;
                }
                var pointOnLineToB = new Point(b.X, y);
                if (!availableArea.Contains(pointOnLineToB))
                {
                    return false;
                }
            }

            return true;
        }
    }
    
    public long SolvePart2V1Working()
    {
        var border = new List<Point>();
        
        var previousPoint = InputEntries[0];
        border.Add(previousPoint);

        // All the input entries are red tiles
        var greenTiles = new List<Point>();
        
        var borderPoints = InputEntries.Skip(1).ToList();
        borderPoints.Add(previousPoint); // Re-include first point so that we wrap around
        
        foreach (var point in borderPoints)
        {
            if (point.X == previousPoint.X)
            {
                // The points are on a vertical line
                var increment = point.Y > previousPoint.Y ? 1 : -1;
                for (var y = previousPoint.Y + increment; y != point.Y; y += increment)
                {
                    var greenTile = new Point(point.X, y);
                    border.Add(greenTile);
                    greenTiles.Add(greenTile);
                }
            }
            else // point.Y == previousPoint.Y
            {
                // The points are on a horizontal line
                var increment = point.X > previousPoint.X ? 1 : -1;
                for (var x = previousPoint.X + increment; x != point.X; x += increment)
                {
                    var greenTile = new Point(x, point.Y);
                    border.Add(greenTile);
                    greenTiles.Add(greenTile);
                }
            }

            border.Add(point);
            previousPoint = point;
        }
        
        var maxArea = 0L;
        
        // Boundary holding the min and max values for X and Y of all our points.
        // We expand the boundary by 1 in each direction, for easier logic later on.
        var boundary = new Boundary(
            border.MinBy(p => p.X).X - 1, border.MinBy(p => p.Y).Y - 1,
            border.MaxBy(p => p.X).X + 1, border.MaxBy(p => p.Y).Y + 1);
        
        // for (var firstPoint = 0; firstPoint < border.Count; firstPoint++)
        for (var firstPoint = 0; firstPoint < InputEntries.Count; firstPoint++)
        {
            // for (var secondPoint = firstPoint + 1; secondPoint < border.Count; secondPoint++)
            for (var secondPoint = firstPoint + 1; secondPoint < InputEntries.Count; secondPoint++)
            {
                // var pointA = border[firstPoint];
                // var pointB = border[secondPoint];
                var pointA = InputEntries[firstPoint];
                var pointB = InputEntries[secondPoint];
                
                if (IsRunningFromTest)
                {
                    IEnumerable<(Point P, char C)> points = border.Select(p =>
                    {
                        var c = p == pointA ? 'A' : (p == pointB ? 'B' : '.');
                        return (p, c);
                    });
                    var a = CalculateArea(pointA, pointB);
                    Console.WriteLine($"\nArea: {a} - Checking if both A ({pointA}) and B ({pointB}) are within the boundary");
                    Helper.PrintMap(boundary, points, printBorder: true);
                }
                
                if (!RectangleIsWithinBorder(pointA, pointB))
                {
                    continue;
                }
                var area = CalculateArea(pointA, pointB);
                maxArea = Math.Max(maxArea, area);
            }
        }

        return maxArea;

        bool RectangleIsWithinBorder(Point a, Point b)
        {
            // Loop first horizontal lines
            var increment = b.X > a.X ? 1 : -1;
            for (var x = a.X; x != b.X; x += increment)
            {
                var pointOnLineFromA = new Point(x, a.Y);
                // if (!PointIsWithinBorder(pointOnLineFromA))
                if (!pointOnLineFromA.IsSurroundedBy(border))
                {
                    return false;
                }
                var pointOnLineToB = new Point(x, b.Y);
                // if (!PointIsWithinBorder(pointOnLineToB))
                if (!pointOnLineToB.IsSurroundedBy(border))
                {
                    return false;
                }
            }
            // The loop vertical lines
            increment = b.Y > a.Y ? 1 : -1;
            for (var y = a.Y; y != b.Y; y += increment)
            {
                var pointOnLineFromA = new Point(a.X, y);
                // if (!PointIsWithinBorder(pointOnLineFromA))
                if (!pointOnLineFromA.IsSurroundedBy(border))
                {
                    return false;
                }
                var pointOnLineToB = new Point(b.X, y);
                // if (!PointIsWithinBorder(pointOnLineToB))
                if (!pointOnLineToB.IsSurroundedBy(border))
                {
                    return false;
                }
            }

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
