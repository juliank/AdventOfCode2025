namespace AdventOfCode.Puzzles;

public class Puzzle06 : Puzzle<string[], long>
{
    private const int PuzzleId = 06;

    public Puzzle06() : base(PuzzleId) { }

    public Puzzle06(params IEnumerable<string[]> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        return CalculateGrandTotal(ParseInputPart1);
    }

    private static IEnumerable<long> ParseInputPart1(IEnumerable<string> problemInput)
    {
        return problemInput.Select(long.Parse);
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    private long CalculateGrandTotal(Func<IEnumerable<string>, IEnumerable<long>> parseInput)
    {
        var grandTotal = 0L;

        var problemInputCount = InputEntries.Count - 1;
        for (var i = 0; i < InputEntries[0].Length; i++)
        {
            var problemOperator = InputEntries[problemInputCount][i]; // The operator is the last row of input items
            var problemInput = InputEntries
                .Select(row => row[i])
                .Take(problemInputCount);
            var parsedInput = parseInput(problemInput)
                .ToArray();
            
            long problemTotal;
            if (problemOperator == "+")
            {
                problemTotal = parsedInput.Sum();
            }
            else // Operator is "*"
            {
                problemTotal = parsedInput.Aggregate(1L, (product, currentInput) => product * currentInput);
            }
            
            grandTotal += problemTotal;
        }

        return grandTotal;
    }

    protected internal override string[] ParseInput(string inputItem)
    {
        var strings = inputItem.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return strings;
    }
}
