namespace AdventOfCode.Utils;

public static class Helper
{
    /// <summary>
    /// Print a map of the given points.
    /// </summary>
    /// <param name="boundary">The size of the map.</param>
    /// <param name="points">The points to mark on the map.</param>
    /// <param name="c">The character to use when marking the points. Default is X.</param>
    /// <param name="w">The character to use as whitespace (positions on the map that are not amon the given points). Default is a space.</param>
    /// <param name="printBorder">Set to true if the print should include a border around the map.</param>
    public static void PrintMap(Boundary boundary, IEnumerable<Point> points, char c = 'X', char w = ' ', bool printBorder = false)
    {
        PrintMap(boundary, points.Select(p => (p, c)), w, printBorder);
    }

    /// <summary>
    /// Print a map of the given points.
    /// </summary>
    /// <param name="boundary">The size of hte maps.</param>
    /// <param name="points">The list of points to print. Each item is a position and a character to print for that position.</param>
    /// <param name="w">The character to use as whitespace (positions on the map that are not amon the given points). Default is a space.</param>
    /// <param name="printBorder">Set to true if the print should include a border around the map.</param>
    public static void PrintMap(Boundary boundary, IEnumerable<(Point P, char C)> points, char w = ' ', bool printBorder = false)
    {
        points = points.ToList(); // Enumerate for better lookup
        if (printBorder)
        {
            Console.WriteLine(new string('=', boundary.MaxX!.Value + 5));
        }

        for (var y = 0; y <= boundary.MaxY; y++)
        {
            var line = new StringBuilder();
            if (printBorder)
            {
                line.Append("| ");
            }

            for (var x = 0; x <= boundary.MaxX; x++)
            {
                var point = points.FirstOrDefault(p => p.P == new Point(x, y));
                var c = point == default ? w : point.C;
                line.Append(c);
            }

            if (printBorder)
            {
                line.Append(" |");
            }
            Console.WriteLine(line.ToString());
        }

        if (printBorder)
        {
            Console.WriteLine(new string('=', boundary.MaxX!.Value + 5));
        }
    }
}
