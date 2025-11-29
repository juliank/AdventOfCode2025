namespace AdventOfCode.Models;

public record class Boundary
{
    public Boundary() { }
    
    public Boundary(int minX, int minY, int maxX, int maxY)
    {
        MaxX = maxX;
        MaxY = maxY;
        MinX = minX;
        MinY = minY;
    }

    public int? MaxX { get; init; }
    public int? MaxY { get; init; }
    public int? MinX { get; init; }
    public int? MinY { get; init; }
    public int? MinZ { get; init; }
    public int? MaxZ { get; init; }
}
