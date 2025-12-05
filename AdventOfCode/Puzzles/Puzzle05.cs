namespace AdventOfCode.Puzzles;

public class Puzzle05 : Puzzle<string, long>
{
    private const int PuzzleId = 05;

    public Puzzle05() : base(PuzzleId) { }

    public Puzzle05(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }
    
    private List<(long Lower, long Upper)> _freshIntervals = [];
    private List<long> _availableIngredients = [];

    public override long SolvePart1()
    {
        ParseIngredients();
        var freshIngredients = 0;

        foreach (var ingredient in _availableIngredients)
        {
            foreach (var interval in _freshIntervals)
            {
                if (ingredient > interval.Upper)
                {
                    // The list is ordered by interval.Upper, so we don't have to check any more now
                    continue;
                }
                
                if (ingredient < interval.Lower)
                {
                    continue;
                }

                freshIngredients++;
                break;
            }
        }

        return freshIngredients;
    }

    public override long SolvePart2()
    {
        ParseIngredients();

        var intervals = _freshIntervals.Select(fi => new Interval(fi.Lower, fi.Upper)).ToList();
        var overlapFound = true;
        while (overlapFound)
        {
            overlapFound = false;
            foreach (var interval in intervals.Where(i => !i.Deleted))
            {
                var overlap = intervals.Except([interval]).FirstOrDefault(i => interval.IsOverlap(i));
                if (overlap != null)
                {
                    overlapFound = true;
                    interval.Merge(overlap);
                }
            }
        }

        var totalFreshIngredients = intervals.Where(i => !i.Deleted).Sum(i => i.Length);
        return totalFreshIngredients;
    }

    public class Interval(long lower, long upper)
    {
        private long Lower { get; set; } = lower;
        private long Upper { get; set; } = upper;
        public bool Deleted { get; private set; }
        public long Length => Upper - Lower + 1;
        
        public bool IsOverlap(Interval interval)
        {
            var lowerIsWithin = interval.Lower >= Lower && interval.Lower <= Upper;
            var upperIsWithin = interval.Upper >= Lower && interval.Upper <= Upper;
            var fullOverlap = interval.Lower < Lower && interval.Upper > Upper;
            return lowerIsWithin  || upperIsWithin || fullOverlap;
        }

        public void Merge(Interval interval)
        {
            Lower = Math.Min(Lower, interval.Lower);
            Upper = Math.Max(Upper, interval.Upper);
            interval.Delete();
        }

        void Delete()
        {
            Lower = -1;
            Upper = -1;
            Deleted = true;
        }
    }

    private void ParseIngredients()
    {
        _freshIntervals = new List<(long Lower, long Upper)>();
        _availableIngredients = new List<long>();

        var processingFresh = true;
        foreach (var inputEntry in InputEntries)
        {
            if (inputEntry == string.Empty)
            {
                processingFresh = false;
                continue;
            }

            if (processingFresh)
            {
                var items = inputEntry.Split('-');
                var lower = long.Parse(items[0]);
                var upper = long.Parse(items[1]);
                _freshIntervals.Add((lower, upper));
            }
            else
            {
                _availableIngredients.Add(long.Parse(inputEntry));
            }
        }
        
        _freshIntervals.Sort((a, b) => a.Upper.CompareTo(b.Upper));
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
