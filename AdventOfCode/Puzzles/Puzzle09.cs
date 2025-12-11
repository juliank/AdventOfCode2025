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
