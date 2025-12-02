namespace AdventOfCode.Puzzles;

public class Puzzle02 : Puzzle<string, long>
{
    private const int PuzzleId = 02;

    public Puzzle02() : base(PuzzleId) { }

    public Puzzle02(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var sum = SumInvalidIds(IsInvalidPart1);
        return sum;
    }

    public override long SolvePart2()
    {
        var sum = SumInvalidIds(IsInvalidPart2);
        return sum;
    }
    
    private long SumInvalidIds(Func<long, bool> isInvalid)
    {
        var ranges = ParseRanges();

        var invalidIds = new List<long>();
        foreach (var range in ranges)
        {
            for (var l = range.lower; l <= range.upper; l++)
            {
                if (isInvalid(l))
                {
                    invalidIds.Add(l);
                }
            }
        }

        var sum = invalidIds.Sum();
        return sum;
    }

    private IEnumerable<(long lower, long upper)> ParseRanges()
    {
        var items = InputEntries[0].Split(',');
        var tupleStrings = items.Select(i => i.Split('-'));
        var ranges = tupleStrings.Select(s =>
        {
            var lower = long.Parse(s[0]);
            var upper = long.Parse(s[1]);
            return (lower, upper);
        });
        return ranges;
    }

    public static bool IsInvalidPart1(long l)
    {
        // any ID which is made only of some sequence of digits repeated twice
        var s = l.ToString();
        if (s.Length % 2 != 0)
        {
            // Odd number of digits cannot be invalid
            return false;
        }
        
        var half = s.Length / 2;
        for (var i = 0; i < half; i++)
        {
            var fromStart = i;
            var fromMid = i + half;
            if (s[fromStart] != s[fromMid])
            {
                return false;
            }
        }

        return true;
    }
    
    public static bool IsInvalidPart2(long l)
    {
        // An ID is invalid if it is made only of some sequence of digits repeated at least twice.
        // So, 12341234 (1234 two times), 123123123 (123 three times), 1212121212 (12 five times),
        // and 1111111 (1 seven times) are all invalid IDs.
        var s = l.ToString();
        var half = s.Length / 2;

        for (var i = 0; i < half; i++)
        {
            var candidateLength = i + 1;
            if (s.Length % candidateLength != 0)
            {
                continue;
            }

            var candidate = s[..candidateLength];
            var candidateLimit = s.Length - candidate.Length;
            var isInvalid = true;
            for (var j = candidateLength; j <= candidateLimit; j += candidateLength)
            {
                var nextCandidate = s[j..(j+candidateLength)];
                if (nextCandidate != candidate)
                {
                    isInvalid = false;
                    break;
                }
            }

            if (isInvalid)
            {
                return true;
            }
        }

        return false;
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
