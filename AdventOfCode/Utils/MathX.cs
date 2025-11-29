/// <summary>
/// Collection of various useful mathematic helper methods
/// </summary>
public static class MathX
{
    public static long LeastCommonMultiple(long[] numbers)
    {
        return numbers.Aggregate(LeastCommonMultiple);
    }

    public static long LeastCommonMultiple(long a, long b)
    {
        return Math.Abs(a * b) / GreatestCommonDivisor(a, b);
    }

    public static long GreatestCommonDivisor(long a, long b)
    {
        return b == 0 ? a : GreatestCommonDivisor(b, a % b);
    }
}
