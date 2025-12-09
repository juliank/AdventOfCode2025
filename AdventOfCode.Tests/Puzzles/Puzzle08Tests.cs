namespace AdventOfCode.Tests.Puzzles;

public class Puzzle08Tests
{
    private readonly Puzzle08 _puzzle;

    public Puzzle08Tests()
    {
        _puzzle = new Puzzle08();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(175440);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(3200955921);
    }

    [Fact]
    public void TestPart1WithExampleInput()
    {
        _puzzle.JunctionBoxesToConnect = 10;
        _puzzle.SetTestInput("""
                             162,817,812
                             57,618,57
                             906,360,560
                             592,479,940
                             352,342,300
                             466,668,158
                             542,29,236
                             431,825,988
                             739,650,466
                             52,470,668
                             216,146,977
                             819,987,18
                             117,168,530
                             805,96,715
                             346,949,466
                             970,615,88
                             941,993,340
                             862,61,35
                             984,92,344
                             425,690,689
                             """);
        var result = _puzzle.SolvePart1();
        result.Should().Be(40);
    }

    [Fact]
    public void TestPart2WithExampleInput()
    {
        _puzzle.SetTestInput("""
                             162,817,812
                             57,618,57
                             906,360,560
                             592,479,940
                             352,342,300
                             466,668,158
                             542,29,236
                             431,825,988
                             739,650,466
                             52,470,668
                             216,146,977
                             819,987,18
                             117,168,530
                             805,96,715
                             346,949,466
                             970,615,88
                             941,993,340
                             862,61,35
                             984,92,344
                             425,690,689
                             """);
        var result = _puzzle.SolvePart2();
        result.Should().Be(25272);
    }
}
