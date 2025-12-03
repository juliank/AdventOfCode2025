namespace AdventOfCode.Puzzles;

public class Puzzle03 : Puzzle<int[], long>
{
    private const int PuzzleId = 03;

    public Puzzle03() : base(PuzzleId) { }

    public Puzzle03(params IEnumerable<int[]> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var joltages = new List<long>();
        foreach (var batteryBank in InputEntries)
        {
            joltages.Add(GetMaxJoltage(batteryBank, 2));
        }
        var somOfJoltages = joltages.Sum();
        return somOfJoltages;
    }

    public override long SolvePart2()
    {
        var joltages = new List<long>();
        foreach (var batteryBank in InputEntries)
        {
            joltages.Add(GetMaxJoltage(batteryBank, 12));
        }
        var sum = joltages.Sum();
        return sum;
    }

    public static long GetMaxJoltage(int[] batteryBank, int batteryCount)
    {
        var numbers = new int[batteryCount];
        
        var indexOfPrevious = -1;
        for (var n = 0; n < batteryCount; n++)
        {
            var lowerIndex = indexOfPrevious + 1;
            var upperIndex = batteryBank.Length - (batteryCount - n);
            for (var i = lowerIndex; i <= upperIndex; i++)
            {
                if (batteryBank[i] > numbers[n])
                {
                    numbers[n] = batteryBank[i];
                    indexOfPrevious = i;
                }
            }
        }

        var sum = long.Parse(string.Join("", numbers.Select(n => n.ToString())));
        return sum;
    }

    protected internal override int[] ParseInput(string inputItem)
    {
        var ints = inputItem.Select(c => int.Parse(c.ToString())).ToArray();
        return ints;
    }
}
