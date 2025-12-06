namespace AdventOfCode.Tests.Puzzles;

public class Puzzle06Tests
{
    private readonly Puzzle06 _puzzle;

    public Puzzle06Tests()
    {
        _puzzle = new Puzzle06();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(4878670269096);
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
        var puzzle = new Puzzle06(
            ["123", "328", "51", "64"],
            ["45", "64", "387", "23"],
            ["6", "98", "215", "314"],
            ["*", "+", "*", "+"]
        );
        var result = puzzle.SolvePart1();
        result.Should().Be(4277556);
    }
}
