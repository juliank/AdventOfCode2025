namespace AdventOfCode.Models;

/// <summary>
/// A two or three dimensional point, with X and Y (and Z) coordinates.
///
/// - X is east (+1) and west (-1).
/// - Y is south (+1) and north (-1)
/// - Z is up (+1) and down (-1)
///
/// Y is perhaps the opposite of what one might expect, but this is due to most
/// of the puzzles in AoC _increases_ the Y value when moving one row _down_
/// in a grid/matrix.
/// </summary>
public readonly record struct Point(int X, int Y, int Z = 0)
{
    public Point Get(Direction direction)
    {
        return direction switch
        {
            Direction.N => GetN(),
            Direction.NE => GetN().GetE(),
            Direction.E => GetE(),
            Direction.SE => GetS().GetE(),
            Direction.S => GetS(),
            Direction.SW => GetS().GetW(),
            Direction.W => GetW(),
            Direction.NW => GetN().GetW(),
            Direction.U => GetUp(),
            Direction.D => GetDown(),
            _ => throw new ArgumentException($"Unknown direction {direction}")
        };
    }

    public Direction GetDirectionTo(Point point)
    {
        if (this.ManhattanDistanceTo(point) != 1)
        {
            throw new InvalidOperationException($"{point} is not an immediate neighbor of {this}");
        }
        if (Z != 0 || point.Z != 0)
        {
            throw new NotImplementedException($"Missing support to find direction from {this} to {point}");
        }

        if (point.X == X) // North or South
        {
            // Higher Y values in the south direction
            return point.Y < Y ? Direction.N : Direction.S;
        }
        if (point.Y == Y) // East or West
        {
            // Higher X values in the east direction
            return point.X < X ? Direction.W : Direction.E;
        }

        throw new NotImplementedException($"Missing support to find direction from {this} to {point}");
    }

    /// <summary>
    /// Check if this point is within the given boundary.
    ///
    /// Being *on* the boundary counts as being within.
    /// </summary>
    /// <remarks>Not to be confused with <see cref="IsSurroundedBy"/></remarks>
    public bool IsWithin(Boundary boundary)
    {
        // Comparing against a nullable that is null will always yield false,
        // so there is no need to check HasValue
        if (X < boundary.MinX || Y < boundary.MinY || Z < boundary.MinZ ||
            X > boundary.MaxX || Y > boundary.MaxY || Z > boundary.MaxZ)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Get the point north (Y-1) of the current
    /// </summary>
    public Point GetN()
    {
        return this with { Y = Y - 1 };
    }

    /// <summary>
    /// Get the point south (Y+1) of the current
    /// </summary>
    public Point GetS()
    {
        return this with { Y = Y + 1 };
    }

    /// <summary>
    /// Get the point east (X+1) of the current
    /// </summary>
    public Point GetE()
    {
        return this with { X = X + 1 };
    }

    /// <summary>
    /// Get the point west (X-1) of the current
    /// </summary>
    public Point GetW()
    {
        return this with { X = X - 1 };
    }

    /// <summary>
    /// Get the point above (Z+1) the current
    /// </summary>
    public Point GetUp()
    {
        return this with { Z = Z + 1 };
    }

    /// <summary>
    /// Get the point below (Z-1) the current
    /// </summary>
    public Point GetDown()
    {
        return this with { Z = Z - 1 };
    }

    public bool IsAdjacentTo(Point point)
    {
        if (this == point)
        {
            return false;
        }

        return Math.Abs(X - point.X) <= 1
            && Math.Abs(Y - point.Y) <= 1
            && Math.Abs(Z - point.Z) <= 1;
    }

    public int ManhattanDistanceTo(Point point)
    {
        return Math.Abs(X - point.X) + Math.Abs(Y - point.Y) + Math.Abs(Z - point.Z);
    }

    public float EuclideanDistanceTo(Point point)
    {
        var v1 = new Vector3(X, Y, Z);
        var v2 = new Vector3(point.X, point.Y, point.Z);
        var distance = Vector3.Distance(v1, v2);
        return distance;
    }

    /// <summary>
    /// Deciding if the point is within a (continuous, and presumably closed) polygon.
    ///
    /// Based on: https://www.eecs.umich.edu/courses/eecs380/HANDOUTS/PROJ2/InsidePoly.html
    /// To determine the status of a point (xp,yp) consider a horizontal ray emanating from (xp,yp) and to the right.
    /// If the number of times this ray intersects the line segments making up the polygon is even then the point is
    /// outside the polygon. Whereas if the number of intersections is odd then the point (xp,yp) lies inside the polygon.
    /// </summary>
    /// <remarks>Not to be confused with <see cref="IsWithin"/></remarks>
    public bool IsSurroundedBy(List<Point> polygon)
    {
        if (Z != 0)
        {
            throw new NotSupportedException($"Currently only supported for 2D points (this point has Z = {Z})");
        }
        
        // Boundary holding the min and max values for X and Y of the polygon,
        // so we know how far we must do the ray tracing.
        var boundary = new Boundary(
            polygon.MinBy(p => p.X).X - 1, polygon.MinBy(p => p.Y).Y - 1,
            polygon.MaxBy(p => p.X).X + 1, polygon.MaxBy(p => p.Y).Y + 1);
        
        // We do a simple ray tracing starting towards the left, until we're outside the boundary
        var currentPoint = new Point(X, Y);
        if (!currentPoint.IsWithin(boundary))
        {
            return false;
        }

        if (polygon.Contains(currentPoint))
        {
            // If the point itself is actually *on* the polygon, we do an early return.
            // Both for simplicity but also because that could mess up the odd/even
            // logic in the algorithm below.
            return true;
        }

        var polygonCrossings = 0;

        var previousOnPolygon = false;
        var enteredAtUpwardsAngle = false;
        var enteredAtDownwardsAngle = false;
        
        while (currentPoint.IsWithin(boundary))
        {
            var northPoint = currentPoint.GetN();
            var southPoint = currentPoint.GetS();
            if (polygon.Contains(currentPoint) && polygon.Contains(northPoint) &&  polygon.Contains(southPoint))
            {
                // We're crossing a vertical piece of the polygon
                polygonCrossings++;
            }
            else if(polygon.Contains(currentPoint))
            {
                if (!previousOnPolygon)
                {
                    // We've entered a horizontal piece of the polygon ...
                    previousOnPolygon = true;
                    // ... and must keep track of the angle at the entering point
                    if (polygon.Contains(northPoint))
                    {
                        enteredAtUpwardsAngle = true;
                    }
                    else if (polygon.Contains(southPoint))
                    {
                        enteredAtDownwardsAngle = true;
                    }
                }
                
                var nextPoint = currentPoint.GetW();
                if (!polygon.Contains(nextPoint))
                {
                    // On the next point of the ray trace we'll be leaving the polygon again
                    if ((polygon.Contains(northPoint) && enteredAtDownwardsAngle) ||
                        (polygon.Contains(northPoint) && enteredAtUpwardsAngle))
                    {
                        // Leaving at an "opposite" angle as the one we entered at means that
                        // we have crossed the polygon
                        polygonCrossings++;
                    }

                    enteredAtUpwardsAngle = false;
                    enteredAtDownwardsAngle = false;
                }
            }
            else
            {
                previousOnPolygon = false;
            }

            currentPoint = currentPoint.GetW();
        }
        
        var isOutsidePolygon = polygonCrossings % 2 == 0;
        return !isOutsidePolygon;
    }
}
