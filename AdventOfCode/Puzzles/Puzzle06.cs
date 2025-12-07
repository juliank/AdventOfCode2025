namespace AdventOfCode.Puzzles;

public class Puzzle06 : Puzzle<string[], long>
{
    private const int PuzzleId = 06;

    public Puzzle06() : base(PuzzleId) { }

    public Puzzle06(params IEnumerable<string[]> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        return CalculateGrandTotal(InputEntries.Select(ie => ie.ToList()).ToList(), ParseInputPart1);
    }

    private static IEnumerable<long> ParseInputPart1(IEnumerable<string> problemInput)
    {
        return problemInput.Select(long.Parse);
    }

    public override long SolvePart2()
    {
        // For parsing part 2, each entry is a single line
        // Add an extra space at the end, for easier logic below
        var inputLines = InputEntries.Select(ie => ie[0] + " ").ToList();
        
        var parsedLines = new List<List<string>>();
        for (var i = 0; i < inputLines.Count; i++)
        {
            parsedLines.Add([]);
        }
        
        var previousIndex = 0;
        var currentIndex = 0;
        // Must do a bit more advanced splitting of each input line to keep all whitespace.
        // A space at the same position on every line indicates a new number.
        while (currentIndex < inputLines[0].Length)
        {
            if (inputLines.All(il => il[currentIndex] == ' '))
            {
                var column = inputLines.Select(il => il[previousIndex..currentIndex]).ToList();
                for (var i = 0; i < column.Count; i++)
                {
                    var row = column[i];
                    parsedLines[i].Add(row);
                }

                previousIndex = currentIndex + 1;
            }

            currentIndex++;
        }
        return CalculateGrandTotal(parsedLines, ParseInputPart2);
    }

    public static IEnumerable<long> ParseInputPart2(IEnumerable<string> problemInput)
    {
        var numbers = new List<string>();
        var column = problemInput.ToList();
        for (var i = 0; i < column[0].Length; i++)
        {
            var number = column.Select(c => c[i]).CreateString().Trim();
            numbers.Add(number);
        }
        return numbers.Select(long.Parse);
    }

    private static long CalculateGrandTotal(List<List<string>> inputEntries, Func<IEnumerable<string>, IEnumerable<long>> parseInput)
    {
        var grandTotal = 0L;

        var problemInputCount = inputEntries.Count - 1;
        for (var i = 0; i < inputEntries[0].Count; i++)
        {
            var problemOperator = inputEntries[problemInputCount][i]; // The operator is the last row of input items
            var problemInput = inputEntries
                .Select(row => row[i])
                .Take(problemInputCount);
            var parsedInput = parseInput(problemInput)
                .ToArray();
            
            long problemTotal;
            if (problemOperator.Trim() == "+")
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

    protected internal override string[] ParseAlternateInput(string inputItem)
    {
        // Parsing the input for part 2 requires all lines to bo considered at the same time.
        // We therefore just have to return a single-entry array here, and then we'll to the
        // actual parsing in SolvePart2
        return [inputItem];
    }
}
