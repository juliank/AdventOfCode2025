namespace AdventOfCode.Models;

public static class Directions
{
    /// <summary>
    /// All possible <see cref="Direction"/> values in a two-dimensional space.
    /// </summary>
    public static readonly Direction[] D2 = [Direction.N, Direction.S, Direction.E, Direction.W];
    public static readonly Direction[] D2Extended = [Direction.N, Direction.NE, Direction.E, Direction.SE, Direction.S, Direction.SW, Direction.W, Direction.NW];

    /// <summary>
    /// All possible <see cref="Direction"/> values in a three-dimensional space.
    /// </summary>
    public static readonly Direction[] D3 = [Direction.N, Direction.S, Direction.E, Direction.W, Direction.U, Direction.D];

    public static Direction Rotate(this Direction direction, int degrees)
    {
        if (degrees is not (0 or 90 or 180 or 270))
        {
            throw new ArgumentOutOfRangeException(nameof(degrees), "Only 0, 90, 180 and 270 are currently supported");
        }

        if (degrees == 0)
        {
            return direction;
        }

        var nextDirection = direction switch
        {
            Direction.N => Direction.E,
            Direction.E => Direction.S,
            Direction.S => Direction.W,
            Direction.W => Direction.N,
            _ => throw new ArgumentException($"Unknown direction {direction}", nameof(direction))
        };
        if (degrees is 180 or 270)
        {
            // Just do one more rotation
            nextDirection = nextDirection switch
            {
                Direction.N => Direction.E,
                Direction.E => Direction.S,
                Direction.S => Direction.W,
                Direction.W => Direction.N,
                _ => throw new ArgumentException($"Unknown direction {direction}", nameof(direction))
            };
        }
        if (degrees is 270)
        {
            // Just do one more rotation
            nextDirection = nextDirection switch
            {
                Direction.N => Direction.E,
                Direction.E => Direction.S,
                Direction.S => Direction.W,
                Direction.W => Direction.N,
                _ => throw new ArgumentException($"Unknown direction {direction}", nameof(direction))
            };
        }
        return nextDirection;
    }
}

public enum Direction
{
    /// <summary>
    /// North => Y - 1
    /// </summary>
    N,
    NE,
    /// <summary>
    /// East => X + 1
    /// </summary>
    E,
    SE,
    /// <summary>
    /// South => Y + 1
    /// </summary>
    S,
    SW,
    /// <summary>
    /// West => X - 1
    /// </summary>
    W,
    NW,
    /// <summary>
    /// Up => Z + 1
    /// </summary>
    U,
    /// <summary>
    /// Down => Z - 1
    /// </summary>
    D
}
