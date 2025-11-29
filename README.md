# Solving a new puzzle

It all starts at https://adventofcode.com/

1. Copy `Puzzle00.cs` and rename it according to the puzzle number
   - Also remember to rename the class inside the file
   - Also copy `Puzzle00Tests.cs` and rename it accordingly (for making sure we don't break old puzzles if refactoring)
1. Save the input for today's puzzle in the parallel `...Input` repository (with the name `NN.txt`)
   - [It is recommended](https://www.reddit.com/r/adventofcode/wiki/faqs/copyright/inputs/) to _not_ share the puzzle input in a public directory
   - The repository for the puzzle inputs should have the same name as this repository, with the added _Input_ postfix
1. Implement `ParseInput` in the newly created puzzle class
1. Implement `SolvePart1` (followed by `SolvePart2`)
1. Run puzzle and submit the result :-)

**_NB: At the start of a new year, when copying files from last year, remember to update `FileHelper.Year`._**

# Running the code

To avoid having to specify the project path every time, it is easiest running the puzzles from within the `AdventOfCode` project directory:

- Run the latest puzzle:  
  `dotnet run`
- Run a specific puzzle:  
  `dotnet run 3`
- Run all tests:  
  `dotnet test ..`

# TODOs

Missing solutions and suggestions for improvements, for when time might be available...

## General

- ...

### Remaining TODOs from last year

- `Puzzle`:
  - Provide a default (virtual) implementation for `ParseInput` => return string item
  - Base class support for `InputMap` is in place. But can this be made more generic?
    - It now only supports `Dictionary<Point, char>` (for when `TInput` is `string`),
      but it could also be useful with `Dictionary<Point, int>` (see e.g. `Puzzle10`).
- `BoundedPoint`: Replace with the new `Boundary` where possible
- `Direction`:
  - Missing comments on new directions
  - Better naming of `D2Extended`? (and similar for 3D?)
- Make it possible to flag a solution (of a part) as "time consuming"?
  - This is done manually today, e.g. for puzzle 6 part 2
  - Only run the actual solution when targeting the specific puzzle
  - When running with "all", return the hard-coded answer
  - When running tests, return the hard-coded answer
- Auto-post answer?
  - Use same session cookie as when auto-loading input
  - Endpoint is `https://adventofcode.com/{YEAR}/day/{DAY}/answer`
  - Request body is `level=1&answer=42`
    - `level` is `1` for part 1 and `2` for part 2
  - Response will return `https://adventofcode.com/{YEAR}/day/{DAY}/answer`
    - HTML body will contain `That's not the right answer; your answer is too high. (...)` if wrong

## Puzzles

- ...