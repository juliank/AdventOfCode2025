namespace AdventOfCode.Tests.Puzzles;

public class Puzzle07Tests
{
    private readonly Puzzle07 _puzzle;

    public Puzzle07Tests()
    {
        _puzzle = new Puzzle07();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(1687);
    }

    // [Fact(Skip = "Not yet implemented")]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(0);
    }

    [Fact]
    public void TestPart1WithExampleInput()
    {
        _puzzle.SetTestInput("""
                             .......S.......
                             ...............
                             .......^.......
                             ...............
                             ......^.^......
                             ...............
                             .....^.^.^.....
                             ...............
                             ....^.^...^....
                             ...............
                             ...^.^...^.^...
                             ...............
                             ..^...^.....^..
                             ...............
                             .^.^.^.^.^...^.
                             ...............
                             """);
        var result = _puzzle.SolvePart1();
        result.Should().Be(21);
    }
}
