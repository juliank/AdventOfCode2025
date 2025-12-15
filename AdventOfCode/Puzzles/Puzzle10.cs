namespace AdventOfCode.Puzzles;

public class Puzzle10 : Puzzle<string, long>
{
    private const int PuzzleId = 10;

    public Puzzle10() : base(PuzzleId) { }

    public Puzzle10(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        throw new NotImplementedException();
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    public static (bool[] State, int[][] WiringSchematics, int[] JoltageRequirements) ParseLine(string lineInput)
    {
        var stateEnd = lineInput.IndexOf(']');
        var desiredState = lineInput[0..stateEnd].Select(c => c == '#').ToArray();
        
        return (desiredState, wiringSchematics, joltageRequirements)
    }

    protected internal override string ParseInput(string inputItem)
    {
        // [.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
        // [...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
        // [.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}
        //
        // [ A ] (B1,B2,B3) (B4,B5) {C1,C2,C3}
        // - A: Indicator lights (desired state, all initially off)
        //      - '.' = off
        //      - '#' = on
        // - B: Button wiring schematics
        //      - Which lights each button will toggle when pressed
        // - C: Joltage requirements
        return inputItem;
    }
}
