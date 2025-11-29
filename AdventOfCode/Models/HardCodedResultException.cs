namespace AdventOfCode.Models;

public class HardCodedResultException : Exception
{
    public object HardcodedResult { get; }

    public HardCodedResultException(object hardcodedResult, string message) : base(message)
    {
        HardcodedResult = hardcodedResult;
    }
}
