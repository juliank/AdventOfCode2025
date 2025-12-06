namespace AdventOfCode.Puzzles;

public class Puzzle06 : Puzzle<string[], long>
{
    private const int PuzzleId = 06;

    public Puzzle06() : base(PuzzleId) { }

    public Puzzle06(params IEnumerable<string[]> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var grandTotal = 0L;

        var problemInputCount = InputEntries.Count - 1;
        for (var i = 0; i < InputEntries[0].Length; i++)
        {
            var problemOperator = InputEntries[problemInputCount][i]; // The operator is the last row of input items
            var problemInput = InputEntries
                .Select(row => row[i])
                .Take(problemInputCount)
                .Select(long.Parse)
                .ToArray();
            
            long problemTotal;
            if (problemOperator == "+")
            {
                problemTotal = problemInput.Sum();
            }
            else // Operator is "*"
            {
                problemTotal = problemInput.Aggregate(1L, (product, currentInput) => product * currentInput);
            }
            
            grandTotal += problemTotal;
        }

        return grandTotal;
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    protected internal override string[] ParseInput(string inputItem)
    {
        var strings = inputItem.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return strings;
    }
}
