namespace AdventOfCode.Tests.Puzzles;

public class Puzzle04Tests
{
    private readonly Puzzle04 _puzzle;

    public Puzzle04Tests()
    {
        _puzzle = new Puzzle04();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(1372);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(7922);
    }

    [Fact]
    public void TestExampleInputPart1()
    {
        var puzzle = new Puzzle04(
            "..@@.@@@@.",
            "@@@.@.@.@@",
            "@@@@@.@.@@",
            "@.@@@@..@.",
            "@@.@@@@.@@",
            ".@@@@@@@.@",
            ".@.@.@.@@@",
            "@.@@@.@@@@",
            ".@@@@@@@@.",
            "@.@.@@@.@."
        );
        
        var result = puzzle.SolvePart1();
        result.Should().Be(13);
    }

    [Fact]
    public void TestExampleInputPart2()
    {
        var puzzle = new Puzzle04(
            "..@@.@@@@.",
            "@@@.@.@.@@",
            "@@@@@.@.@@",
            "@.@@@@..@.",
            "@@.@@@@.@@",
            ".@@@@@@@.@",
            ".@.@.@.@@@",
            "@.@@@.@@@@",
            ".@@@@@@@@.",
            "@.@.@@@.@."
        );
        
        var result = puzzle.SolvePart2();
        result.Should().Be(43);
    }
}
