namespace AdventOfCode.Utils;

public class ProgressLogger
{
    private long _intervalCount;
    private long _totalCount;

    private readonly Stopwatch _intervalStopwatch = Stopwatch.StartNew();
    private readonly Stopwatch _totalStopwatch = Stopwatch.StartNew();

    public long? MaxCount { get; init; }
    public long? LogIntervalCount { get; init; }
    public TimeSpan? LogIntervalTime { get; init; }

    /// <summary>
    /// The logger is ready for use immediately after instantiation. But if the logging
    /// isn't supposed to start at that point, calling RestartLogger will re-initialize
    /// all counters and timers.
    /// </summary>
    public void RestartLogger()
    {
        _intervalCount = 0;
        _totalCount = 0;
        _intervalStopwatch.Restart();
        _totalStopwatch.Restart();
    }

    public void IncrementAndLog(long increment = 1, string? currentValue = null)
    {
        _intervalCount += increment;
        _totalCount += increment;

        if (ShouldLog())
        {
            LogProgress(currentValue);
        }

        if (_intervalCount >= LogIntervalCount || _intervalStopwatch.Elapsed >= LogIntervalTime)
        {
            _intervalCount = 0;
            _intervalStopwatch.Restart();
        }
    }

    private bool ShouldLog()
    {
        if (LogIntervalCount.HasValue)
        {
            return _intervalCount >= LogIntervalCount;
        }

        if (LogIntervalTime.HasValue)
        {
            return _intervalStopwatch.Elapsed >= LogIntervalTime;
        }

        return true;
    }

    public void LogProgress(string? currentValue = null)
    {
        var intervalElapsed = _intervalStopwatch.Elapsed;
        var totalElapsed = _totalStopwatch.Elapsed;
        var intervalRate = (float)(_intervalCount / intervalElapsed.TotalSeconds);
        var totalRate = (float)(_totalCount / totalElapsed.TotalSeconds);

        var current = string.IsNullOrEmpty(currentValue) ? "" : $"{currentValue}. ";
        var progress = $"{current}Processed {_intervalCount:N0} items in {intervalElapsed}, rate {intervalRate:N0} items/s)";
        if (LogIntervalCount.HasValue || LogIntervalTime.HasValue)
        {
            // In this case, the (current) interval count and the total count will differ
            progress += $" (total {_totalCount:N0} items in {totalElapsed}, rate {totalRate:N0} items/s)";
        }

        if (MaxCount.HasValue)
        {
            var remainingCount = MaxCount.Value - _totalCount;
            var remainingTime = remainingCount / intervalRate;
            progress += $". Remaining: {remainingCount} items, estimated {remainingTime})";
        }

        progress += ".";
        Console.WriteLine(progress);
    }
}
