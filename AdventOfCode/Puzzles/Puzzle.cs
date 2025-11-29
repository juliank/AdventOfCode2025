[assembly: InternalsVisibleTo("AdventOfCode.Tests")]
namespace AdventOfCode.Puzzles;

/// <summary>
/// Base class for puzzle implementations, offering generically typed versions
/// of the different solve methods.
/// </summary>
/// <typeparam name="TInput">The type each line of the puzzle input will be parsed as.</typeparam>
/// <typeparam name="TResult">The type of the result from solving the puzzle, typically an int or a long.</typeparam>
public abstract class Puzzle<TInput, TResult>(int id) : IPuzzle
    where TInput : notnull
    where TResult : notnull
{
    public int Id { get; } = id;

    private List<TInput>? _inputEntries;

    /// <summary>
    /// The list of parsed entries for the puzzle input, one item per line in the input.
    /// </summary>
    /// <remarks>The input file is loaded and parsed the first time the property is read.</remarks>
    public List<TInput> InputEntries
    {
        get
        {
            _inputEntries ??= LoadInput();
            return _inputEntries;
        }
        protected set
        {
            _inputEntries = value;
        }
    }

    private readonly Dictionary<Point, char> _inputMap = [];

    /// <summary>
    /// For puzzles where the input is treated like a block of text (i.e. lines
    /// of strings), this property can be used as a map where the key is a point
    /// in the map and the value is the character value at that specific point.
    /// </summary>
    protected Dictionary<Point, char> InputMap
    {
        get
        {
            if (typeof(TInput) != typeof(string))
            {
                throw new ArgumentException($"{nameof(InputMap)} can only be used when {nameof(TInput)} is of the type string.");
            }
            if (_inputMap.Count == 0)
            {
                ProcessMapInput();
            }
            return _inputMap;
        }
    }

    /// <summary>
    /// Boundary for <see cref="InputMap"/>.
    /// </summary>
    protected Boundary Boundary
    {
        get
        {
            if (_inputMap.Count == 0)
            {
                // Touching input map, to get both the type check and loading of the map
                var _ = InputMap;
            }
            return field;
        }

        private set;
    } = new();

    private void ProcessMapInput()
    {
        // InputEntries[y][x] (rows,columns)
        var rows = InputEntries.Count;
        var columns = (InputEntries[0] as string)!.Length;
        Boundary = new Boundary
        {
            MinX = 0, MaxX = columns - 1,
            MinY = 0, MaxY = rows - 1
        };

        for (var y = 0; y < rows; y++)
        {
            for (var x = 0; x < columns; x++)
            {
                var position = new Point(x, y);
                var plant = (InputEntries[y] as string)![x];
                _inputMap.Add(position, plant);
            }
        }
    }

    /// <summary>
    /// Alternate ctor to allow creating the puzzle with a hardcode set of entries (for easier testing).
    /// </summary>
    protected Puzzle(int id, params IEnumerable<TInput> inputEntries) : this(id)
    {
        _inputEntries = inputEntries.ToList();
    }

    object IPuzzle.Solve() => Solve();

    public virtual bool SkipPart1WhenSolveAll => false;

    object IPuzzle.SolvePart1() => SolvePart1();

    public virtual bool SkipPart2WhenSolveAll => false;

    object IPuzzle.SolvePart2() => SolvePart2();

    /// <summary>
    /// Will first try and solve part 2 of the puzzle. If part 2 isn't implemented,
    /// part 1 will be solved instead.
    /// </summary>
    public TResult Solve()
    {
        TResult result;

        var sw = Stopwatch.StartNew();
        string? hardCoded = null;
        try
        {
            try
            {
                result = SolvePart2();
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Solution to part 2 is not implemented yet, solving part 1 instead");
                sw = Stopwatch.StartNew();
                result = SolvePart1();
            }
        }
        catch (HardCodedResultException e)
        {
            result = (TResult)e.HardcodedResult;
            hardCoded = e.Message;
        }

        sw.Stop();
        var elapsed = sw.Elapsed < TimeSpan.FromSeconds(1) ? $"{sw.ElapsedMilliseconds} ms" : $"{sw.Elapsed}";
        var time = hardCoded == null ? $"{elapsed}" : $"hard-coded ({hardCoded})";
        Console.WriteLine($"Solution time: {time}");

        return result;
    }

    protected internal bool IsRunningFromTest { get; set; }

    /// <summary>
    /// Throws a <see cref="HardCodedResultException"/> with the given result.
    /// </summary>
    /// <param name="hardCodedResult">The result to throw.</param>
    /// <param name="message">An optional message to include in the exception.</param>
    /// <param name="notFromTest">If true, the exception will not be thrown when running the puzzle from the test project.</param>
    /// <remarks>
    /// This method is useful if it is necessary to do an "early return", instead of running the rest of the implemented
    /// logic. E.g. when it is too slow (for when running all the puzzles in batch), or the solution only works
    /// (efficiently) on the test input.
    /// </remarks>
    protected void ThrowHardCodedResult(TResult hardCodedResult, string message = "Result is hard-coded", bool notFromTest = false)
    {
        if (notFromTest && IsRunningFromTest)
        {
            // Let the normal execution continue...
            return;
        }
        
        throw new HardCodedResultException(hardCodedResult, message);
    }

    /// <summary>
    /// Implement this method to solve part 1 of the puzzle.
    /// </summary>
    public abstract TResult SolvePart1();

    /// <summary>
    /// Implement this method to solve part 2 of the puzzle.
    /// </summary>
    public abstract TResult SolvePart2();

    /// <summary>
    /// Implement this method to parse the puzzle's input. The method should parse
    /// a single line from the input file.
    /// </summary>
    /// <remarks>
    /// Override <see cref="ParseAlternateInput(string)"/> to use a different parsing logic for part 2.
    /// </remarks>
    /// <param name="line">One line from the puzzle input.</param>
    protected internal abstract TInput ParseInput(string line);

    /// <summary>
    /// Override this method to provide alternative parsing logic for when solving part 2.
    /// </summary>
    /// <param name="line">One line from the puzzle input.</param>
    protected internal virtual TInput ParseAlternateInput(string line) => ParseInput(line);

    private string[]? _testInput;

    internal void SetTestInput(params IEnumerable<string> testInput)
    {
        _inputEntries = null; // Must clear, in case the has been set using the alternate constructor
        _testInput = testInput.ToArray();
    }

    private List<TInput> LoadInput()
    {
        var baseMethod = typeof(Puzzle<,>).GetMethod(nameof(ParseAlternateInput), BindingFlags.Instance | BindingFlags.NonPublic)!;
        var derivedMethod = GetType().GetMethod(nameof(ParseAlternateInput), BindingFlags.Instance | BindingFlags.NonPublic)!;

        var isInvokedFromPart2 = new StackTrace().GetFrames().Any(f => f.GetMethod()!.Name == nameof(SolvePart2));
        var alternateParsingImplemented = baseMethod.DeclaringType!.Name != derivedMethod.DeclaringType!.Name;
        var useAlternateParsing = isInvokedFromPart2 && alternateParsingImplemented;

        string[]? lines = _testInput;
        if (lines == null)
        {
            var path = FileHelper.GetInputFilePath(Id);
            lines = File.ReadAllLines(path);
        }

        var entries = new List<TInput>();
        foreach (var line in lines)
        {
            var parsedInput = useAlternateParsing ? ParseAlternateInput(line) : ParseInput(line);
            entries.Add(parsedInput);
        }

        return entries;
    }
}
