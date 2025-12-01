namespace AdventOfCode.Puzzles;

public class Puzzle01 : Puzzle<int, int>
{
    private const int PuzzleId = 01;

    public Puzzle01() : base(PuzzleId) { }

    public Puzzle01(params IEnumerable<int> inputEntries) : base(PuzzleId, inputEntries) { }

    public override int SolvePart1()
    {
        // Dial starts at 50
        // L == -1
        // R == +1
        // 0-99 around

        var timesAtZero = 0;
        var position = 50;
        foreach (var i in InputEntries)
        {
            position += i;
            while (position > 99)
            {
                position -= 100;
            }
            while (position < 0)
            {
                position += 100;
            }
            if (position == 0)
            {
                timesAtZero++;
            }
        }

        return timesAtZero;
    }
    
    public override int SolvePart2()
    {
        var timesAtZero = 0;
        var position = 50;

        foreach (var inputEntry in InputEntries)
        {
            var rotations = inputEntry;
            while (rotations != 0)
            {
                if (rotations > 0)
                {
                    position++;
                    rotations--;
                    if (position == 100)
                    {
                        position = 0;
                    }
                }
                else
                {
                    position--;
                    rotations++;
                    if (position == -1)
                    {
                        position = 99;
                    }
                }

                if (position == 0)
                {
                    timesAtZero++;
                }
            }
        }
        
        return timesAtZero;
    }

    protected internal override int ParseInput(string inputItem)
    {
        var direction = inputItem[0] == 'L' ? -1 : 1;
        var value = int.Parse(inputItem[1..]);
        return direction * value;
    }
}
