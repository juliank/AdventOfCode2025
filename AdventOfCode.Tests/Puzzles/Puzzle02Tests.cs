namespace AdventOfCode.Tests.Puzzles;

public class Puzzle02Tests
{
    private readonly Puzzle02 _puzzle;

    public Puzzle02Tests()
    {
        _puzzle = new Puzzle02();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(28146997880);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(40028128307);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(11, true)]
    [InlineData(1010, true)]
    [InlineData(1001, false)]
    [InlineData(101010, false)]
    public void TestIsInvalid1(long l, bool isInvalid)
    {
        Puzzle02.IsInvalidPart1(l).Should().Be(isInvalid);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(11, true)]
    [InlineData(1010, true)]
    [InlineData(1001, false)]
    [InlineData(101010, true)]
    [InlineData(12341234, true)]
    [InlineData(123123123, true)]
    [InlineData(1212121212, true)]
    [InlineData(1111111, true)]
    [InlineData(110, false)]
    public void TestIsInvalid2(long l, bool isInvalid)
    {
        Puzzle02.IsInvalidPart2(l).Should().Be(isInvalid);
    }
    
    [Theory]
    [InlineData(11, 22, 11, 22)]
    [InlineData(99, 115, 99, 111)]
    public void TestIsInvalid2ForRange(int lower, int upper, params int[] invalidIds)
    {
        for (var i = lower; i <= upper; i++)
        {
            var isInvalid = invalidIds.Contains(i);
            var result = isInvalid ? "invalid" : "valid";
            Puzzle02.IsInvalidPart2(i).Should().Be(isInvalid, $"{i} should be {result}");
        }
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
