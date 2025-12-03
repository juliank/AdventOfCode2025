namespace AdventOfCode.Tests.Puzzles;

public class Puzzle03Tests
{
    private readonly Puzzle03 _puzzle;

    public Puzzle03Tests()
    {
        _puzzle = new Puzzle03();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(16973);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(168027167146027);
    }

    [Fact]
    public void Test1WithExampleInput()
    {
        List<int[]> exampleInput =
        [
            [9, 8, 7, 6, 5, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1],
            [8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9],
            [2, 3, 4, 2, 3, 4, 2, 3, 4, 2, 3, 4, 2, 7, 8],
            [8, 1, 8, 1, 8, 1, 9, 1, 1, 1, 1, 2, 1, 1, 1]
        ];
        var puzzle = new Puzzle03(exampleInput);
        var part1 = puzzle.SolvePart1();
        part1.Should().Be(357);
    }

    [Fact]
    public void Test2WithExampleInput()
    {
        List<int[]> exampleInput =
        [
            [9, 8, 7, 6, 5, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1],
            [8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9],
            [2, 3, 4, 2, 3, 4, 2, 3, 4, 2, 3, 4, 2, 7, 8],
            [8, 1, 8, 1, 8, 1, 9, 1, 1, 1, 1, 2, 1, 1, 1]
        ];
        var puzzle = new Puzzle03(exampleInput);
        var part1 = puzzle.SolvePart2();
        part1.Should().Be(3121910778619);
    }

    [Theory]
    [InlineData(98, 9, 8, 7, 6, 5, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1)]
    [InlineData(89, 8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9)]
    [InlineData(78, 2, 3, 4, 2, 3, 4, 2, 3, 4, 2, 3, 4, 2, 7, 8)]
    [InlineData(92, 8, 1, 8, 1, 8, 1, 9, 1, 1, 1, 1, 2, 1, 1, 1)]
    public void TestGetMaxJoltage(int maxJoltage, params int[] batteryBank)
    {
        var result = Puzzle03.GetMaxJoltage(batteryBank, 2);
        result.Should().Be(maxJoltage);
    }
}
