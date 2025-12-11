namespace AdventOfCode.Tests.Puzzles;

public class Puzzle09Tests
{
    private readonly Puzzle09 _puzzle;

    public Puzzle09Tests()
    {
        _puzzle = new Puzzle09();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(4759420470);
    }

    // [Fact(Skip = "Not yet implemented")]
    [Fact]
    public void SolvePart2()
    {
        _puzzle.IsRunningFromTest = true;
        var result = _puzzle.SolvePart2();
        result.Should().BeGreaterThan(0);
    }

    [Fact]
    public void TestPart1WithExampleInput()
    {
        _puzzle.SetTestInput("""
                             7,1
                             11,1
                             11,7
                             9,7
                             9,5
                             2,5
                             2,3
                             7,3
                             """);
        var result = _puzzle.SolvePart1();
        result.Should().Be(50);
    }

    [Fact]
    public void TestPart2WithExampleInput()
    {
        _puzzle.IsRunningFromTest = true; // Enables debug output
        _puzzle.SetTestInput("""
                             7,1
                             11,1
                             11,7
                             9,7
                             9,5
                             2,5
                             2,3
                             7,3
                             """);
        var result = _puzzle.SolvePart2();
        result.Should().Be(24);
    }
}
