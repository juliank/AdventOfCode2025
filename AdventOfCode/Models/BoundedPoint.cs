namespace AdventOfCode.Models;

public record class BoundedPoint(Point Point)
{
    public int? MaxX { get; init; }
    public int? MaxY { get; init; }
    public int? MinX { get; init; }
    public int? MinY { get; init; }
    public int? MinZ { get; init; }
    public int? MaxZ { get; init; }

    public bool TryGetDirection(Direction direction, out Point p)
    {
        p = Point.Get(direction);
        return !(p.X > MaxX ||
                 p.X < MinX ||
                 p.Y > MaxY ||
                 p.Y < MinY ||
                 p.Z > MaxZ ||
                 p.Z < MinZ);
    }

    /// <summary>
    /// Get the point <see cref="Direction.N"/> of the current. The return value indicates
    /// if the point exists within the defined boundaries.
    /// </summary>
    public bool TryGetN(out Point p)
    {
        p = Point.GetN();
        return MinY == null || Point.Y - 1 >= MinY;
    }

    /// <summary>
    /// Get the point <see cref="Direction.S"/> of the current. The return value indicates
    /// if the point exists within the defined boundaries.
    /// </summary>
    public bool TryGetS(out Point p)
    {
        p = Point.GetS();
        return MaxY == null || Point.Y + 1 <= MaxY;
    }

    /// <summary>
    /// Get the point <see cref="Direction.E"/> of the current. The return value indicates
    /// if the point exists within the defined boundaries.
    /// </summary>
    public bool TryGetE(out Point p)
    {
        p = Point.GetE();
        return MaxX == null || Point.X + 1 <= MaxX;
    }

    /// <summary>
    /// Get the point <see cref="Direction.W"/> of the current. The return value indicates
    /// if the point exists within the defined boundaries.
    /// </summary>
    public bool TryGetW(out Point p)
    {
        p = Point.GetW();
        return MinX == null || Point.X - 1 >= MinX;
    }

    /// <summary>
    /// Get the point <see cref="Direction.U"/> of the current. The return value indicates
    /// if the point exists within the defined boundaries.
    /// </summary>
    public bool TryGetUp(out Point p)
    {
        p = Point.GetUp();
        return MaxZ == null || Point.Z + 1 <= MaxZ;
    }

    /// <summary>
    /// Get the point <see cref="Direction.D"/> of the current. The return value indicates
    /// if the point exists within the defined boundaries.
    /// </summary>
    public bool TryGetDown(out Point p)
    {
        p = Point.GetDown();
        return MinZ == null || Point.Z - 1 >= MinZ;
    }
}
