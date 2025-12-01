namespace AdventOfCode.Tests.Puzzles;

public class Puzzle01Tests
{
    private readonly Puzzle01 _puzzle;

    public Puzzle01Tests()
    {
        _puzzle = new Puzzle01();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(1097);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(7101);
    }

    [Theory]
    [InlineData(1, 50)]
    [InlineData(1, -50)]
    [InlineData(1, 100)]
    [InlineData(1, -100)]
    [InlineData(2, 150)]
    [InlineData(2, -150)]
    [InlineData(3, 250)]
    [InlineData(3, -250)]
    [InlineData(2, -50, -100)]
    [InlineData(2, 50, 100)]
    [InlineData(2, -49, -1, -1, 1)]
    public void Test1(int expected, params int[] inputEntries)
    {
        var puzzle = new Puzzle01(inputEntries);
        var result = puzzle.SolvePart2();
        result.Should().Be(expected);
    }
}
