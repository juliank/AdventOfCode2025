namespace AdventOfCode.Tests.Puzzles;

public class Puzzle05Tests
{
    private readonly Puzzle05 _puzzle;

    public Puzzle05Tests()
    {
        _puzzle = new Puzzle05();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(674);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(352509891817881);
    }
    
    [Fact]
    public void TestPart1WithExampleInput()
    {
        var puzzle = new Puzzle05(
            "3-5",
            "10-14",
            "16-20",
            "12-18",
            "",
            "1",
            "5",
            "8",
            "11",
            "17",
            "32"
        );
        var result = puzzle.SolvePart1();
        result.Should().Be(3);
    }
    
    [Fact]
    public void TestPart2WithExampleInput()
    {
        var puzzle = new Puzzle05(
            "3-5",
            "10-14",
            "16-20",
            "12-18",
            "",
            "1",
            "5",
            "8",
            "11",
            "17",
            "32"
        );
        var result = puzzle.SolvePart2();
        result.Should().Be(14);
    }
}
