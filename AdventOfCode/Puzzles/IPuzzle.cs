namespace AdventOfCode.Puzzles;

/// <summary>
/// Any class implementing the IPuzzle interface will be discovered and run
/// by the main program.
/// </summary>
public interface IPuzzle
{
    object Solve();
    object SolvePart1();
    object SolvePart2();
    bool SkipPart1WhenSolveAll { get; }
    bool SkipPart2WhenSolveAll { get; }
}
