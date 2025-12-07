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

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(8674740488592);
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
    
    // 123 328  51 64 
    //  45 64  387 23 
    //   6 98  215 314
    // *   +   *   +  
    [Theory]
    [InlineData(new[] { "64 ", "23 ", "314" }, new long[] { 4, 431, 623 })]
    [InlineData(new[] { " 51", "387", "215" }, new long[] { 175, 581, 32 })]
    [InlineData(new[] { "328", "64 ", "98 " }, new long[] { 8, 248, 369 })]
    [InlineData(new[] { "123", " 45", "  6" }, new long[] { 356, 24, 1 })]
    
    public void TestPart2Parsing(string[] input, long[] parsedInput)
    {
        var result = Puzzle06.ParseInputPart2(input);
        result.Should().BeEquivalentTo(parsedInput);
    }
}
