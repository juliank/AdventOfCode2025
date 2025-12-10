// ReSharper disable GrammarMistakeInComment
namespace AdventOfCode.Tests.Models;

public class PointTests
{
    [Theory]
    // Points along the horizontal line through the middle of the polygon
    [InlineData(0, 2, false)]
    [InlineData(1, 2, true)]
    [InlineData(2, 2, true)]
    [InlineData(3, 2, true)]
    [InlineData(4, 2, false)]
    // Points that are fully above/below the polygon
    [InlineData(2, 0, false)]
    [InlineData(2, 4, false)]
    // Points that start on the same horizontal line as the top/bottom of the polygon
    [InlineData(4, 1, false)]
    [InlineData(4, 3, false)]
    public void IsSurroundedBy_ForPolygon1_ReturnsExpected(int x, int y, bool expected)
    {
        // Create a polygon consisting of a simple square:
        //
        // Y\X 0 1 2 3 4 X
        //  0 
        //  1    . . .
        //  2    .   .
        //  3    . . .
        //  4
        //  Y
        List<Point> polygon =
            [
                new(1, 1), new(2, 1), new(3, 1),
                new(1, 2),            new(3, 2),
                new(1, 3), new(2, 3), new(3, 3)
            ];
        var isSurroundedBy = new Point(x, y).IsSurroundedBy(polygon);
        isSurroundedBy.Should().Be(expected);
    }
    
    [Theory]
    // Simple alternatives with only "direct" crossings of the polygon
    [InlineData(0, 3, false)]
    [InlineData(1, 3, true)]
    [InlineData(3, 3, true)]
    [InlineData(5, 3, true)]
    [InlineData(6, 3, false)]
    // Point outside, but leftwards ray tracing will partially traverse the polygon (from (5,2) to (3,2)
    [InlineData(6, 2, false)]
    public void IsSurroundedBy_ForPolygon2_ReturnsExpected(int x, int y, bool expected)
    {
        // Create a polygon consisting of a simple square:
        //
        // Y\X 0 1 2 3 4 5 6 X
        //  0 
        //  1    . . .
        //  2    .   . . .
        //  3    .       .
        //  4    . . . . .
        //  5
        //  Y
        List<Point> polygon =
        [
            new(1, 1), new(2, 1), new(3, 1),
            new(1, 2),            new(3, 2), new(4, 2), new(5, 2),
            new(1, 3),                                  new(5, 3),
            new(1, 4), new(2, 4), new(3, 4), new(4, 4), new(5, 4)
        ];
        var isSurroundedBy = new Point(x, y).IsSurroundedBy(polygon);
        isSurroundedBy.Should().Be(expected);
    }
}
