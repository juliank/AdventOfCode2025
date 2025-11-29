namespace AdventOfCode.Utils;

public static class ExtensionMethods
{
    public static string CreateString(this IEnumerable<char> chars)
    {
        var s = new string(chars.ToArray());
        return s;
    }

    public static int ParseInt(this IEnumerable<char> chars)
    {
        var s = new string(chars.ToArray());
        return int.Parse(s);
    }

    public static long ParseLong(this IEnumerable<char> chars)
    {
        var s = new string(chars.ToArray());
        return long.Parse(s);
    }

    public static bool TryParseInt(this IEnumerable<char> chars, out int result)
    {
        var s = new string(chars.ToArray());
        if (int.TryParse(s, out var i))
        {
            result = i;
            return true;
        }

        result = 0;
        return false;
    }

    public static bool TryParseLong(this IEnumerable<char> chars, out long result)
    {
        var s = new string(chars.ToArray());
        if (long.TryParse(s, out var l))
        {
            result = l;
            return true;
        }

        result = 0;
        return false;
    }

    // https://stackoverflow.com/a/3683217
    public static IEnumerable<TResult> SelectWithPrevious<TSource, TResult>(this IEnumerable<TSource> source,
     Func<TSource, TSource, TResult> projection)
    {
        using var iterator = source.GetEnumerator();
        if (!iterator.MoveNext())
        {
            yield break;
        }
        TSource previous = iterator.Current;
        while (iterator.MoveNext())
        {
            yield return projection(previous, iterator.Current);
            previous = iterator.Current;
        }
    }
}
