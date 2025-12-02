namespace AdventOfCode.Tests.Puzzles;

public class Puzzle02Tests
{
    private readonly Puzzle02 _puzzle;

    public Puzzle02Tests()
    {
        _puzzle = new Puzzle02();
    }

    // [Fact(Skip = "Not yet implemented")]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(0);
    }

    // [Fact(Skip = "Not yet implemented")]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(0);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(11, true)]
    [InlineData(1010, true)]
    [InlineData(1001, false)]
    public void TestIsInvalid(long l, bool isInvalid)
    {
        Puzzle02.IsInvalidPart1(l).Should().Be(isInvalid);
    }

    [Fact]
    public void Test1WithExampleInput()
    {
        var exampleInput = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,\n1698522-1698528,446443-446449,38593856-38593862,565653-565659,\n824824821-824824827,2121212118-2121212124";
        var puzzle = new Puzzle02(exampleInput);
        var part1 = puzzle.SolvePart1();
        part1.Should().Be(1227775554);
    }

    [Fact]
    public void Test2WithExampleInput()
    {
        var exampleInput = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,\n1698522-1698528,446443-446449,38593856-38593862,565653-565659,\n824824821-824824827,2121212118-2121212124";
        var puzzle = new Puzzle02(exampleInput);
        var part1 = puzzle.SolvePart2();
        part1.Should().Be(4174379265);
    }
}
