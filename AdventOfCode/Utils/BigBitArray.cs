using System.Collections;

namespace AdventOfCode.Utils;

public class BigBitArray
{
    private readonly int _minX;
    private readonly int _minY;
    private readonly long _width; // Use long as type, to force type in later calculations
    private readonly BitArray[] _arrays;

    public BigBitArray(int minX, int minY, int maxX, int maxY)
    {
        _minX = minX;
        _minY = minY;
        _width = maxX - minX + 1;
        var height = maxY - minY + 1;
        var maxSize = _width * height;

        var bitArraysNeeded = maxSize / int.MaxValue + 1;
        if (bitArraysNeeded > 10)
        {
            // An arbitrarily chosen limit, just for the sake of some limit.
            // A BitArray of the size int.MaxValue will be roughly ... MB,
            // 10 seems like a reasonable limit
            throw new ArgumentOutOfRangeException($"Maximum array size ({maxSize}) will require {bitArraysNeeded} bit arrays. Maximum 10 is supported.");
        }
        
        Console.WriteLine($"{DateTime.Now.TimeOfDay}: Creating {bitArraysNeeded} bit arrays of size {int.MaxValue}");
        _arrays = new BitArray[bitArraysNeeded];
        for (var i = 0; i < bitArraysNeeded; i++)
        {
            // All will be false by default
            _arrays[i] = new BitArray(int.MaxValue);
            Console.WriteLine($"{DateTime.Now.TimeOfDay}: Created #{i + 1}");
        }
    }

    private long IndexOf(int x, int y)
    {
        // Use long for the multiply to avoid overflow before casting to int
        var idx = (y - _minY) * _width + (x - _minX);
        return idx;
    }

    public long SetCount { get; private set; }
    
    public void Set(int x, int y)
    {
        SetCount++;
        if (SetCount % 1_000 == 0)
        {
            Console.WriteLine($"Set called {SetCount} times (current point: {x},{y})");
        }
        
        var index = IndexOf(x, y);
        var arrayIndex = index / int.MaxValue;
        var indexInArray = checked((int)(index % int.MaxValue));
        var array = _arrays[arrayIndex];
        array[indexInArray] = true;
    }

    public bool Contains(int x, int y)
    {
        var index = IndexOf(x, y);
        var arrayIndex = index / int.MaxValue;
        var indexInArray = checked((int)(index % int.MaxValue));
        var array = _arrays[arrayIndex];
        return array[indexInArray];
    }
}
